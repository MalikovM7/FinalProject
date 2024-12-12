using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Repositories.Repositories;
using System.Linq.Expressions;

namespace Persistence.Repositories.Implementations
{
    public class NewsRepository : BaseRepository<News>, INewsRepository
    {
        public NewsRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<int> CountAsync(Expression<Func<News, bool>> predicate)
        {
            return await _context.Set<News>().CountAsync(predicate);
        }
    }
}
