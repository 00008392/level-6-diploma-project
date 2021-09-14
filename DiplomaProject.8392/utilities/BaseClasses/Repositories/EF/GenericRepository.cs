using BaseClasses.Contracts;
using BaseClasses.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseClasses.Repositories.EF
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
                                                         
    {
        private readonly DbContext _context;
        public GenericRepository(DbContext context)
        {
            _context = context;
        }
        private DbSet<T> _dbSet => _context.Set<T>();

        public async Task AddRangeAsync(ICollection<T> items)
        {
            _dbSet.AddRange(items);
            await _context.SaveChangesAsync();
        }
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
            return await GetDbSetWithRelatedTables(includes).ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id, params Expression<Func<T, object>>[] includes)
        {
            return await GetDbSetWithRelatedTables(includes).AsNoTracking().SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<ICollection<T>> GetFilteredAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            var list = await GetDbSetWithRelatedTables(includes).Where(filter).ToListAsync();
            return list;
        }

        public bool DoesItemWithIdExist(long id)
        {
            return _dbSet.Any(t => t.Id == id);
        }
        public async Task RemoveRangeAsync(ICollection<T> items)
        {
            _dbSet.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        private IQueryable<T> GetDbSetWithRelatedTables(params Expression<Func<T, object>>[] includes)
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
