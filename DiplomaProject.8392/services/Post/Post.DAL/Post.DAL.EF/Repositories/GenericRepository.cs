using Microsoft.EntityFrameworkCore;
using Post.DAL.EF.Data;
using Post.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Post.DAL.EF.Repositories
{
    public class GenericRepository<T> : BaseRepository<T>, IRepository<T>
        where T : BaseEntity
    {
        public GenericRepository(PostDbContext context):base(context)
        {
        }

        public async Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            return await Include(includes).ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id, params Expression<Func<T, object>>[] includes)
        {
            return await Include(includes).SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<ICollection<T>> GetFilteredAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            var list = await Include(includes).Where(filter).ToListAsync();
            return list;
        }
        private IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query,
                  (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
