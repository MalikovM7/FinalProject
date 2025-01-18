using Domain.Models;
using FinalProjectMVC.ViewModels.Admin.Car;
using FinalProjectMVC.ViewModels.Admin.Testimonials;
using FinalProjectMVC.ViewModels.Vehicles;
using Microsoft.AspNetCore.Mvc;
using Services.Implementations.Implementations;
using Services.Interfaces;

namespace FinalProjectMVC.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly IVehicleService _vehicleService;
        private readonly ITestimonialService _testimonialService;

        public VehicleController(ILogger<VehicleController> logger, IVehicleService vehicleService, ITestimonialService testimonialService)
        {

            _vehicleService = vehicleService;
            _logger = logger;
            _testimonialService = testimonialService;
        }

    
        [HttpGet]
        public async Task<IActionResult> Vehicle(decimal? minPrice, decimal? maxPrice)
        {
            var cars = (await _vehicleService.GetCarSAsync())?.ToList() ?? new List<Car>();

            var minAvailablePrice = cars.Any() ? cars.Min(car => car.PricePerDay) : 0;
            var maxAvailablePrice = cars.Any() ? cars.Max(car => car.PricePerDay) : 0;

            if (minPrice.HasValue && maxPrice.HasValue)
            {
                cars = cars.Where(car => car.PricePerDay >= minPrice && car.PricePerDay <= maxPrice).ToList();
            }

            var approvedTestimonials = (await _testimonialService.GetTestimonalAsync())
                .Where(t => t.IsApproved).ToList();

            var model = new VehiclePageVM
            {
                Cars = cars,
                Testimonials = approvedTestimonials,
                MinPrice = minAvailablePrice,
                MaxPrice = maxAvailablePrice
            };

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var car = await _vehicleService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            var viewModel = new VehicleDetailsVM
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
                CategoryName = car.Category?.Name // Assuming category is a navigation property
            };

            return View(viewModel); 
        }


    }
}
