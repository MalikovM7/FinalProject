using Domain.Identity;
using Domain.Models;
using FinalProjectMVC.ViewModels.Admin.Car;
using FinalProjectMVC.ViewModels.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace FinalProjectMVC.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly UserManager<AppUser> _userManager;

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
        public async Task<IActionResult> Reserve(Reservation reservation)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
                await _reservationService.ReserveCarAsync(reservation, user);
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

            var activeReservations = reservations
                .Where(r => r.EndDate >= DateTime.UtcNow.Date)
                .Select(r => new CarReservationViewModel
                {
                    CarId = r.CarId,
                    Brand = r.Car.Brand,
                    Model = r.Car.Model,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    PricePerDay = r.Car.PricePerDay,
                    TotalPrice = r.TotalPrice,
                    Location = r.Car.Location
                })
                .ToList();

            // Pass the correct model to the view
            var viewModel = new ReservePageViewModel
            {
                ReservationList = activeReservations
            };

            return View(viewModel);
        }
    }
}
