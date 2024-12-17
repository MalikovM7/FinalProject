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

namespace Services.Implementations.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ReservationService(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Retrieves a list of available cars for a given date range.
        /// </summary>
        public async Task<List<Car>> GetAvailableCarsAsync(DateTime startDate, DateTime endDate)
        {
            // Query cars that are available and have no overlapping reservations
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
    }
}