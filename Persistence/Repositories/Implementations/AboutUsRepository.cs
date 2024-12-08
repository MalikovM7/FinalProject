using Persistence.Data;
using Domain.Models;
using Repositories.Repositories;


namespace Persistence.Repositories.Implementations
{
    public class AboutUsRepository : BaseRepository<AboutUsViewModel>, IAboutUsRepository
    {

        public AboutUsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
