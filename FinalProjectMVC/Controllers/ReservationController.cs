using Domain.Identity;
using Domain.Models;
using FinalProjectMVC.ViewModels.Admin.Car;
using FinalProjectMVC.ViewModels.Reservation;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Text.RegularExpressions;

namespace FinalProjectMVC.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly UserManager<AppUser> _userManager;
        //private readonly IValidator<CarReservationViewModel> _validator;

        public ReservationController(IReservationService reservationService, UserManager<AppUser> userManager)
        {
            _reservationService = reservationService;
            _userManager = userManager;
            
        }

        // Display available cars based on date range
        [HttpGet]
        public async Task<IActionResult> AvailableCars(DateTime startDate, DateTime endDate)
        {
            var model = new ReservePageViewModel
            {
                AvailableCars = new List<VehicleVM>(),
                Reservation = new CarReservationViewModel
                {
                    StartDate = startDate,
                    EndDate = endDate
                }
            };

            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue || startDate > endDate)
            {
                ModelState.AddModelError("", "Please provide valid start and end dates.");
                return View(model);
            }

            var cars = await _reservationService.GetAvailableCarsAsync(startDate, endDate);
            model.AvailableCars = cars.Select(c => new VehicleVM
            {
                Id = c.Id,
                Brand = c.Brand,
                Model = c.Model,
                PricePerDay = c.PricePerDay,
                Location = c.Location
            }).ToList();

            return View(model);
        }

        // Display the reservation page
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Reserve(int carId, DateTime startDate, DateTime endDate)
        {
            var car = (await _reservationService.GetAvailableCarsAsync(startDate, endDate))
                        .FirstOrDefault(c => c.Id == carId);

            if (car == null)
            {
                ModelState.AddModelError("", "The selected car is not available.");
                return RedirectToAction(nameof(AvailableCars), new { startDate, endDate });
            }

            var viewModel = new CarReservationViewModel
            {
                CarId = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                PricePerDay = car.PricePerDay,
                StartDate = startDate,
                EndDate = endDate,
                Location = car.Location,
                TotalPrice = (endDate - startDate).Days * car.PricePerDay
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Reserve(CarReservationViewModel model)
        {
            // Validate model state
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Validate phone number
            if (string.IsNullOrWhiteSpace(model.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "Phone number is required.");
                return View(model);
            }

            var phoneRegex = @"^\+994(50|51|55|70|77|10)[1-9][0-9]{6}$";
            if (!Regex.IsMatch(model.PhoneNumber, phoneRegex))
            {
                ModelState.AddModelError("PhoneNumber", "Invalid phone number format. Example: +994514443024.");
                return View(model);
            }

            // Get the authenticated user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            string filePath = null;

            // Validate and save driving license file
            if (model.DrivingLicense != null && model.DrivingLicense.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
                var fileExtension = Path.GetExtension(model.DrivingLicense.FileName).ToLower();

                // Validate file extension
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("DrivingLicense", "Only JPG, PNG, and PDF files are allowed.");
                    return View(model);
                }

                // Validate file size (max 5MB)
                if (model.DrivingLicense.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("DrivingLicense", "File size cannot exceed 5 MB.");
                    return View(model);
                }

                // Save the file to the server
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/licenses");
                Directory.CreateDirectory(uploadsFolder); // Ensure the directory exists
                string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.DrivingLicense.FileName)}";
                filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.DrivingLicense.CopyToAsync(stream);
                }

                // Store the relative path for easier access
                filePath = $"/uploads/licenses/{uniqueFileName}";
            }
            else
            {
                ModelState.AddModelError("DrivingLicense", "Driving license upload is required.");
                return View(model);
            }

            // Create and save the reservation
            var reservation = new Reservation
            {
                CarId = model.CarId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                TotalPrice = model.TotalPrice,
                PhoneNumber = model.PhoneNumber,
                DrivingLicensePath = filePath, // Use the relative path
                Status = "Pending"
            };

            await _reservationService.ReserveCarAsync(reservation, user);

            // Notify the user of successful submission
            TempData["SuccessMessage"] = "Your reservation has been submitted and is pending admin approval.";
            return RedirectToAction(nameof(MyReservations));
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyReservations()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var reservations = await _reservationService.GetUserReservationsAsync(user.Id);

            // Ensure reservations include the Status property
            var reservationList = reservations
                .Select(r => new CarReservationViewModel
                {
                    CarId = r.CarId,
                    Brand = r.Car.Brand,
                    Model = r.Car.Model,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    PricePerDay = r.Car.PricePerDay,
                    TotalPrice = r.TotalPrice,
                    Location = r.Car.Location,
                    Status = r.Status // Ensure Status is being passed correctly
                })
                .ToList();

            var viewModel = new ReservePageViewModel
            {
                ReservationList = reservationList
            };

            return View(viewModel);
        }
    }
}
