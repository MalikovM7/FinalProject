using Domain.Models;
using FinalProjectMVC.ViewModels.Admin.Car;
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
            //if (carVM.CategoryId == 0)
            //{
            //    ModelState.AddModelError("CategoryId", "Please select a category.");
            //}

            //if (carVM.Brand is null || carVM.Color is null || carVM.Fueltype is null) return View(carVM);

            

            var car = new Car
            {
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
        public async Task<IActionResult> AvailableCars(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                ModelState.AddModelError("", "Start date cannot be later than the end date.");
                return View();
            }

            var availableCars = await _vehicleService.GetAvailableCarsAsync(startDate, endDate);

            // Ensure the list is not null
            var carVMs = (availableCars ?? new List<Car>()).Select(c => new VehicleVM
            {
                Id = c.Id,
                Brand = c.Brand,
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

            return View("AvailableCarsList", carVMs);
        }
    }
}
