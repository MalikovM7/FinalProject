using Domain.Models;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using Persistence.Data;
using Microsoft.AspNetCore.Identity;
using Repositories.Repositories;

namespace Services.Implementations.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;
        private readonly IReservationRepository _reservationRepository;
        private readonly UserManager<AppUser> _userManager;

        public ReservationService(AppDbContext context, UserManager<AppUser> userManager, IReservationRepository reservationRepository)
        {
            _context = context;
            _userManager = userManager;
            _reservationRepository = reservationRepository;
        }

 
        public async Task<List<Car>> GetAvailableCarsAsync(DateTime startDate, DateTime endDate)
        {
            
            var availableCars = await _context.Cars
                .Where(c => c.IsAvailable &&
                            !_context.Reservations.Any(r => r.CarId == c.Id &&
                                                            r.StartDate < endDate &&
                                                            r.EndDate > startDate))
                .ToListAsync();

            return availableCars;
        }


        public async Task<Reservation> ReserveCarAsync(Reservation reserve , AppUser user)
        {
            var car = await _context.Cars.FindAsync(reserve.CarId);
            var totalDays = (reserve.EndDate - reserve.StartDate).Days;
            var totalPrice = totalDays * car.PricePerDay;


            var data = new Reservation
            {
                CarId = reserve.CarId,
                StartDate = reserve.StartDate,
                EndDate = reserve.EndDate,
                CreatedDate = DateTime.Now,
                TotalPrice = totalPrice,
                UserId = user.Id,
                PhoneNumber = reserve.PhoneNumber,
                DrivingLicensePath = reserve.DrivingLicensePath,
               
            };
            
            await _context.Reservations.AddAsync(data);
            await _context.SaveChangesAsync();

            return data;
        }


        public async Task<IEnumerable<Reservation>> GetUserReservationsAsync(string userId)
        {
            var userReservations = await _context.Reservations
                .Include(r => r.Car)
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return userReservations;
        }


        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            var allReservations = await _context.Reservations
                .Include(r => r.Car)
                .Include(r => r.AppUser)
                .ToListAsync();

            return allReservations;
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
           return await _reservationRepository.GetByIdAsync(id);
        }

        public async Task DeleteReservationAsync(int id)
        {
            await _reservationRepository.DeleteAsync(id);
        }




        public async Task UpdateReservationAsync(Reservation reservation)
        {
            var existingReservation = await _context.Reservations.FindAsync(reservation.Id);
            if (existingReservation == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }

            
            existingReservation.Status = reservation.Status;

            
            _context.Reservations.Update(existingReservation);
            await _context.SaveChangesAsync();
        }




    }
}