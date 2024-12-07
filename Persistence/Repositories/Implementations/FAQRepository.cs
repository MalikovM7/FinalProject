using FinalProjectMVC.Data;
using FinalProjectMVC.Models;
using Repositories.Repositories;

namespace FinalProjectMVC.Repositories.Implementations
{
    public class FAQRepository : BaseRepository<FAQ>, IFaqRepository
    {
        public FAQRepository(AppDbContext context) : base(context)
        {
        }
    }
}
