using Domain.Models;
using Persistence.Data;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Implementations
{
    public class VehicleRepository : BaseRepository<Car>, IVehicleRepository
    {
        public VehicleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
