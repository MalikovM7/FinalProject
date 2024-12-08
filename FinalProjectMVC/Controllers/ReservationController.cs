using Domain.Identity;
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

        public async Task<IActionResult> AvailableCars()
        {
            var cars = await _reservationService.GetAvailableCarsAsync();
            return View(cars);
        }

        public async Task<IActionResult> Reserve(int carId)
        {
            var cars = await _reservationService.GetAvailableCarsAsync();
            var car = cars.FirstOrDefault(c => c.Id == carId);

            if (car == null)
                return NotFound();

            var viewModel = new CarReservationViewModel
            {
                CarId = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                PricePerDay = car.PricePerDay,
                AvailabilityStart = car.AvailabilityStart,
                AvailabilityEnd = car.AvailabilityEnd,
                Location = car.Location
            };

            return View(viewModel);
        }

       [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Reserve(CarReservationViewModel viewModel, DateTime startDate, DateTime endDate)
{
    var user = await _userManager.GetUserAsync(User);
    if (user == null)
        return Unauthorized();

    try
    {
        await _reservationService.ReserveCarAsync(viewModel.CarId, user, startDate, endDate);
        return RedirectToAction("MyReservations");
    }
    catch (InvalidOperationException ex)
    {
        ModelState.AddModelError(string.Empty, ex.Message);
        return View(viewModel); // Pass the ViewModel back to the view
    }
}

        [Authorize]
        public async Task<IActionResult> MyReservations()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var reservations = await _reservationService.GetUserReservationsAsync(user.Id);
            return View(reservations);
        }
    }
}
