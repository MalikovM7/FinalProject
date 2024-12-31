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
        public async Task<IActionResult>Index()
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
            var car = new Car
            {
                Brand = carVM.Brand,
                Model = carVM.Model,
                Color = carVM.Color,
                Year = carVM.Year,
                Fueltype = carVM.Fueltype,
                LicensePlate = carVM.LicensePlate,
                PricePerDay = carVM.PricePerDay,
                IsAvailable = true, // Ensure new vehicle is always available
                ImagePath = carVM.ImagePath,
                Location = carVM.Location,
                AvailabilityStart = DateTime.Now, // Default to now
                AvailabilityEnd = null, // No end by default
                CategoryId = carVM.CategoryId
            };

            await _vehicleService.AddCarAsync(car);
            return RedirectToAction(nameof(Index));
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
                CategoryId = car.CategoryId
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

        // GET: Admin/Car/Delete/5
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

        // POST: Admin/Car/Delete/5
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

        // POST: Admin/Vehicle/AvailableCars
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AvailableCars(DateTime? startDate, DateTime? endDate)
        {
            var model = new ReservePageViewModel
            {
                Reservation = new CarReservationViewModel
                {
                    StartDate = startDate ?? DateTime.Today,
                    EndDate = endDate ?? DateTime.Today.AddDays(1)
                },
                AvailableCars = new List<VehicleVM>() // Initialize an empty list to avoid null
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

            // Update availability details
            car.IsAvailable = carVM.IsAvailable;
            car.AvailabilityStart = carVM.AvailabilityStart ?? DateTime.Now;
            car.AvailabilityEnd = carVM.AvailabilityEnd;

            await _vehicleService.UpdateCarAsync(carVM.Id, car);

            return RedirectToAction(nameof(Index));
        }











    }
}
