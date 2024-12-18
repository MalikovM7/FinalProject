using FinalProjectMVC.ViewModels.Admin.News;
using FinalProjectMVC.ViewModels.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace FinalProjectMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class ReservationController : Controller
    {

        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var reservationVM = new CarReservationViewModel
            {
                   CarId = reservation.CarId,
                   UserId = reservation.UserId,
                   TotalPrice = reservation.TotalPrice,
                   StartDate = reservation.StartDate,
                   EndDate = reservation.EndDate,

            };

            return View(reservationVM);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
