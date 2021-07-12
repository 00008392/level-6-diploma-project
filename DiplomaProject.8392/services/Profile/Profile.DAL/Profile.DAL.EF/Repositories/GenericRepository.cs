using Microsoft.EntityFrameworkCore;
using Profile.DAL.EF.Data;
using Profile.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Profile.DAL.EF.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T: BaseEntity
    {
        private readonly ProfileDbContext _context;
        public GenericRepository(ProfileDbContext context)
        {
            _context = context;
        }
        private DbSet<T> _dbSet => _context.Set<T>();
        public async Task CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
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

        public bool IfExists(long id)
        {
            return _dbSet.Any(t => t.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        private IQueryable<T> Include( params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = null;
            foreach (var include in includes)
            {
                query = _dbSet.Include(include);
            }

            return query == null ? _dbSet : query;
        }
    }
}
