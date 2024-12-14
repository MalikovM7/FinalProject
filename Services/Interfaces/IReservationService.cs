
using Domain.Models;
using Domain.Identity;


namespace Services.Interfaces
{
    public interface IReservationService
    {

        Task<IEnumerable<Car>> GetAvailableCarsAsync();
        Task<Reservation> ReserveCarAsync(int carId, AppUser user, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Reservation>> GetUserReservationsAsync(string userId);

        Task<IEnumerable<Reservation>> GetReservationsAsync();



    }
}
