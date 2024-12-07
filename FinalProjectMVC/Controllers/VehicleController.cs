using FinalProjectMVC.Models;
using FinalProjectMVC.Services.Implementations;
using FinalProjectMVC.ViewModels.Home;
using FinalProjectMVC.ViewModels.Vehicles;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace FinalProjectMVC.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly IVehicleService _vehicleService;

        public VehicleController(ILogger<VehicleController> logger, IVehicleService vehicleService)
        {

            _vehicleService = vehicleService;
        }

        public async Task<IActionResult> Vehicle()
        {
            var cars = await _vehicleService.GetVehiclesAsync();

            if (cars == null || !cars.Any())
            {
                _logger.LogWarning("No Cars found in the database.");
            }

            var model = new VehiclePageVM
            {
                Cars = cars ?? Enumerable.Empty<Car>(),
            };


            return View(model);
        }
    }
}
