
using Domain.Models;
using Domain.Identity;



namespace Services.Interfaces
{
    public interface IReservationService
    {

        Task<List<Car>> GetAvailableCarsAsync(DateTime startDate, DateTime endDate);

        // Reserve a car for a user
        Task<Reservation> ReserveCarAsync(Reservation reserve, AppUser user);

        // Get reservations for a specific user
        Task<IEnumerable<Reservation>> GetUserReservationsAsync(string userId);

        // Get all reservations (e.g., for administrative purposes)
        Task<IEnumerable<Reservation>> GetReservationsAsync();

        Task<Reservation> GetReservationByIdAsync(int id);

        Task DeleteReservationAsync(int id);



    }
}
