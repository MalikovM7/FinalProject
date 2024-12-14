using Domain.Models;
using Persistence.Data;
using Domain.Identity;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;

        public ReservationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAvailableCarsAsync()
        {
            return await _context.Cars
                .Where(c => c.IsAvailable && c.AvailabilityStart <= DateTime.Now && c.AvailabilityEnd >= DateTime.Now)
                .ToListAsync();
        }

        public async Task<Reservation> ReserveCarAsync(int carId, AppUser user, DateTime startDate, DateTime endDate)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == carId);
            if (car == null || !car.IsAvailable || startDate >= endDate || startDate < car.AvailabilityStart || endDate > car.AvailabilityEnd)
            {
                throw new InvalidOperationException("Invalid reservation details.");
            }

            var days = (endDate - startDate).Days;
            var totalPrice = days * car.PricePerDay;

            var reservation = new Reservation
            {
                CarId = carId,
                UserId = user.Id,
                StartDate = startDate,
                EndDate = endDate,
                TotalPrice = totalPrice
            };

            car.IsAvailable = false;

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }

        public async Task<IEnumerable<Reservation>> GetUserReservationsAsync(string userId)
        {
            return await _context.Reservations
                .Include(r => r.Car)
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            return (await _context.Reservations.ToListAsync());
        }
    }
}
