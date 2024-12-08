using Persistence.Data;
using Domain.Models;
using Repositories.Repositories;

namespace Persistence.Repositories.Implementations
{
    public class FAQRepository : BaseRepository<FAQ>, IFaqRepository
    {
        public FAQRepository(AppDbContext context) : base(context)
        {
        }
    }
}
