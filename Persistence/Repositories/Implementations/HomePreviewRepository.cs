using Persistence.Data;
using Domain.Models;
using Repositories.Repositories;

namespace Persistence.Repositories.Implementations
{
    public class HomePreviewRepository : BaseRepository<HomePreview>, IHomePreviewRepository
    {
        public HomePreviewRepository(AppDbContext context) : base(context)
        {
        }
    }
}
