using Domain.Models;
using Persistence.Data;
using Repositories.Repositories;


namespace Persistence.Repositories.Implementations
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
