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
    public abstract class BaseRepository<T>: IBaseRepository<T> where T: BaseEntity
    {
        protected readonly PostDbContext _context;
        public BaseRepository(PostDbContext context)
        {
            _context = context;
        }
        protected DbSet<T> _dbSet => _context.Set<T>();
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
        public bool IfExists(long id)
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
      
    }
}
