using FinalProjectMVC.Common;
using FinalProjectMVC.Data;
using FinalProjectMVC.Exceptions;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories;
using System.Linq.Expressions;

namespace FinalProjectMVC.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id) ?? throw new NotFoundException(ExceptionMessages.NotFoundMessage);

            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();

        }

        public async Task EditAsync(T entity)
        {
            _dbSet.Update(entity);

            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithExpression(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }



        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.AsQueryable();


            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var entity = await query.FirstOrDefaultAsync(e => e.Id == id);
            return entity ?? throw new NotFoundException(ExceptionMessages.NotFoundMessage);
        }

        public async Task<T> GetWithExpression(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }


    }

}
