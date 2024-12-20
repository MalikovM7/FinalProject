﻿using Domain.Exceptions;
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
            var reservation = await _reservationService.GetReservationsAsync();
            var reservationVM = reservation.Select(x => new ReservationVM
            {
                Id = x.Id,
                CarId = x.CarId,
                UserId = x.UserId,
                TotalPrice = x.TotalPrice,
                StartDate = x.StartDate,
                EndDate = x.EndDate,

            }).ToList();

            return View(reservationVM);
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
    }
}
