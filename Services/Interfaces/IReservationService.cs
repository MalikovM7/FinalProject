
using Domain.Models;
using Domain.Identity;



namespace Services.Interfaces
{
    public interface IReservationService
    {

        Task<List<Car>> GetAvailableCarsAsync(DateTime startDate, DateTime endDate);

        
        Task<Reservation> ReserveCarAsync(Reservation reserve, AppUser user);

    
        Task<IEnumerable<Reservation>> GetUserReservationsAsync(string userId);

       
        Task<IEnumerable<Reservation>> GetReservationsAsync();

        Task<Reservation> GetReservationByIdAsync(int id);

        Task DeleteReservationAsync(int id);


        Task UpdateReservationAsync(Reservation reservation);
    }
}
