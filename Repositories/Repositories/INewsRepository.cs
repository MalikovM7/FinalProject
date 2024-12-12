using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public interface INewsRepository : IBaseRepository<News>
    {
        Task<int> CountAsync(Expression<Func<News, bool>> predicate);
    }
}
