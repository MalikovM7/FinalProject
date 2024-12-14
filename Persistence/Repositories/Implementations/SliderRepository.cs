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
    public class SliderRepository : BaseRepository<Slider>, ISliderRepository
    {

        public SliderRepository(AppDbContext context) : base(context)
        {

        }
        public Task<bool> ChangeMainStatusAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
