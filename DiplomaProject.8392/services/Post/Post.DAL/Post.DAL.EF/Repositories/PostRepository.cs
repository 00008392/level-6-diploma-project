
using Microsoft.EntityFrameworkCore;
using Post.DAL.EF.Data;
using Post.Domain.Core;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Post.DAL.EF.Repositories
{
    public class PostRepository :  IPostRepository
    {
        protected readonly PostDbContext _context;
       
        public PostRepository(PostDbContext context)
        {
            _context = context;
        }
        protected DbSet<Accommodation> _dbSet => _context.Set<Accommodation>();
        public async Task AddRangeAsync(ICollection<Accommodation> items)
        {
            _dbSet.AddRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(Accommodation entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Accommodation entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public bool DoesItemWithIdExist(long id)
        {
            return _dbSet.Any(t => t.Id == id);
        }

        public async Task RemoveRangeAsync(ICollection<Accommodation> items)
        {
            _dbSet.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Accommodation entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Accommodation>> GetAllAsync()
        {
            return await GetDbSetWithRelatedTables().ToListAsync();
        }

        public async Task<Accommodation> GetByIdAsync(long id)
        {
            return await GetDbSetWithRelatedTables().SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<ICollection<Accommodation>> GetFilteredAsync(Expression<Func<Accommodation, bool>> filter)
        {
            return await GetDbSetWithRelatedTables().Where(filter).ToListAsync();
        }

        private IQueryable<Accommodation> GetDbSetWithRelatedTables()
        {
           return _dbSet.Include(x => x.Owner).Include(x => x.Category)
                .Include(x => x.AccommodationPhotos)
                .Include(x => x.AccommodationRules).ThenInclude(r => r.Item)
                .Include(x => x.AccommodationFacilities).ThenInclude(f => f.Item)
                .Include(x => x.AccommodationSpecificities).ThenInclude(s => s.Item);
        }

    }
}
