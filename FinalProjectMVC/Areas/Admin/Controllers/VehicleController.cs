using Domain.Models;
using FinalProjectMVC.ViewModels.Admin.Car;
using FinalProjectMVC.ViewModels.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;

namespace FinalProjectMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
          _vehicleService = vehicleService;
        }
        public async Task<IActionResult> Index()
        {
            var cars = await _vehicleService.GetCarSAsync();
            var carVMs = cars.Select(c => new VehicleVM
            {
                Id = c.Id,
                Brand = c.Brand,
                Model = c.Model,
                Color = c.Color,
                Year = c.Year,
                Fueltype = c.Fueltype,
                LicensePlate = c.LicensePlate,
                PricePerDay = c.PricePerDay,
                IsAvailable = c.IsAvailable,
                ImagePath = c.ImagePath,
                Location = c.Location,
                AvailabilityStart = c.AvailabilityStart,
                AvailabilityEnd = c.AvailabilityEnd,
                CategoryName = c.Category?.Name 
            }).ToList();

            return View(carVMs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var car = await _vehicleService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            var carVM = new VehicleVM
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Color = car.Color,
                Year = car.Year,
                Fueltype = car.Fueltype,
                LicensePlate = car.LicensePlate,
                PricePerDay = car.PricePerDay,
                IsAvailable = car.IsAvailable,
                ImagePath = car.ImagePath,
                Location = car.Location,
                AvailabilityStart = car.AvailabilityStart,
                AvailabilityEnd = car.AvailabilityEnd,
                CategoryName = car.Category?.Name
            };

            return View(carVM);
        }


        public async Task<IActionResult> CreateAsync()
        {
            var categories = await _vehicleService.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleVM carVM)
        {
            if (!ModelState.IsValid)
            {
               
                return View(carVM);
            }

            // Custom validations
            if (carVM.Year > DateTime.Now.Year || carVM.Year < 1900)
            {
                ModelState.AddModelError("Year", "The year must be between 1900 and the current year.");
            }

            if (carVM.PricePerDay <= 0)
            {
                ModelState.AddModelError("PricePerDay", "Price per day must be a positive value.");
            }

            if (!IsValidLicensePlate(carVM.LicensePlate))
            {
                ModelState.AddModelError("LicensePlate", "Invalid license plate format.");
            }

            if (!string.IsNullOrEmpty(carVM.ImagePath) && !IsValidImagePath(carVM.ImagePath))
            {
                ModelState.AddModelError("ImagePath", "Invalid image path.");
            }

            if (!ModelState.IsValid)
            {
                
                return View(carVM);
            }

            var car = new Car
            {
                Brand = carVM.Brand,
                Model = carVM.Model,
                Color = carVM.Color,
                Year = carVM.Year,
                Fueltype = carVM.Fueltype,
                LicensePlate = carVM.LicensePlate,
                PricePerDay = carVM.PricePerDay,
                IsAvailable = true,
                ImagePath = carVM.ImagePath,
                Location = carVM.Location,
                AvailabilityStart = DateTime.Now,
                AvailabilityEnd = null,
                CategoryId = carVM.CategoryId
            };

            await _vehicleService.AddCarAsync(car);
            return RedirectToAction(nameof(Index));
        }

        
        private bool IsValidLicensePlate(string licensePlate)
        {
          
            return !string.IsNullOrEmpty(licensePlate) && licensePlate.Length > 3;
        }

        private bool IsValidImagePath(string imagePath)
        {
          
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            return validExtensions.Any(ext => imagePath.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var car = await _vehicleService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            var carVM = new VehicleVM
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Color = car.Color,
                Year = car.Year,
                Fueltype = car.Fueltype,
                LicensePlate = car.LicensePlate,
                PricePerDay = car.PricePerDay,
                IsAvailable = car.IsAvailable,
                ImagePath = car.ImagePath,
                Location = car.Location,
                AvailabilityStart = car.AvailabilityStart,
                AvailabilityEnd = car.AvailabilityEnd,
                CategoryId = car.CategoryId,
                CategoryName = car.Category?.Name
            };

            var categories = await _vehicleService.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", car.CategoryId);
            return View(carVM);
        }


       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleVM carVM)
        {
            if (id != carVM.Id)
            {
                return NotFound();
            }

            var car = new Car
            {
                Id = carVM.Id,
                Brand = carVM.Brand,
                Model = carVM.Model,
                Color = carVM.Color,
                Year = carVM.Year,
                Fueltype = carVM.Fueltype,
                LicensePlate = carVM.LicensePlate,
                PricePerDay = carVM.PricePerDay,
                IsAvailable = carVM.IsAvailable,
                ImagePath = carVM.ImagePath,
                Location = carVM.Location,
                AvailabilityStart = carVM.AvailabilityStart,
                AvailabilityEnd = carVM.AvailabilityEnd,
                CategoryId = carVM.CategoryId
            };

            await _vehicleService.UpdateCarAsync(id, car);
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _vehicleService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            var carVM = new VehicleVM
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Color = car.Color,
                Year = car.Year,
                Fueltype = car.Fueltype,
                LicensePlate = car.LicensePlate,
                PricePerDay = car.PricePerDay,
                IsAvailable = car.IsAvailable,
                ImagePath = car.ImagePath,
                Location = car.Location,
                AvailabilityStart = car.AvailabilityStart,
                AvailabilityEnd = car.AvailabilityEnd,
                CategoryName = car.Category?.Name
            };

            return View(carVM);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleService.DeleteCarAsync(id);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult AvailableCars()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AvailableCars(DateTime? startDate, DateTime? endDate)
        {
          
            var reservationStartDate = startDate ?? DateTime.Today;
            var reservationEndDate = endDate ?? DateTime.Today.AddDays(1);

            
            var availableCars = (await _vehicleService.GetAvailableCarsAsync(reservationStartDate, reservationEndDate))
                .Select(car => new VehicleVM
                {
                    Id = car.Id,
                    Brand = car.Brand,
                    Model = car.Model,
                    PricePerDay = car.PricePerDay,
                    Location = car.Location,
                    IsAvailable = car.IsAvailable
                }).ToList();

           
            var model = new ReservePageViewModel
            {
                Reservation = new CarReservationViewModel
                {
                    StartDate = reservationStartDate,
                    EndDate = reservationEndDate
                },
                AvailableCars = availableCars // Assign the mapped cars
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Adjust(int id)
        {
            var car = await _vehicleService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            var carVM = new VehicleVM
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                IsAvailable = car.IsAvailable,
                AvailabilityStart = car.AvailabilityStart,
                AvailabilityEnd = car.AvailabilityEnd
            };

            return View(carVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adjust(VehicleVM carVM)
        {
            var car = await _vehicleService.GetCarByIdAsync(carVM.Id);
            if (car == null)
            {
                return NotFound();
            }

            
            car.IsAvailable = carVM.IsAvailable;
            car.AvailabilityStart = carVM.AvailabilityStart ?? DateTime.Now;
            car.AvailabilityEnd = carVM.AvailabilityEnd;

            await _vehicleService.UpdateCarAsync(carVM.Id, car);

            return RedirectToAction(nameof(Index));
        }











    }
}
