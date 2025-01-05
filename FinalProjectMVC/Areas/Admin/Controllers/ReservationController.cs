using Domain.Exceptions;
using Domain.Models;
using FinalProjectMVC.ViewModels.Admin.News;
using FinalProjectMVC.ViewModels.Admin.Reservation;
using FinalProjectMVC.ViewModels.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Implementations.Implementations;
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
        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationService.GetReservationsAsync();
            var reservationVMs = reservations.Select(r => new ReservationVM
            {
                Id = r.Id,
                CarId = r.CarId,
                UserId = r.UserId,
                TotalPrice = r.TotalPrice,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                Status = r.Status
            }).ToList();

            return View(reservationVMs);
        }


        public async Task<IActionResult> Delete(int id)
        {
           
                var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound("Reservation not found.");
            }

            var reservationVM = new ReservationVM
                {
                    Id= reservation.Id,
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

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound("Reservation not found.");
            }

            reservation.Status = "Approved";
            await _reservationService.UpdateReservationAsync(reservation);

            TempData["SuccessMessage"] = "Reservation approved successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Decline(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound("Reservation not found.");
            }

            reservation.Status = "Declined";
            await _reservationService.UpdateReservationAsync(reservation);

            TempData["SuccessMessage"] = "Reservation declined successfully!";
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound("Reservation not found.");
            }

            var reservationVM = new ReservationVM
            {
                Id = reservation.Id,
                CarId = reservation.CarId,
                UserId = reservation.UserId,
                TotalPrice = reservation.TotalPrice,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Status = reservation.Status,
                PhoneNumber = reservation.PhoneNumber,
                DrivingLicensePath = reservation.DrivingLicensePath
            };

            return View(reservationVM);
        }





    }
}
