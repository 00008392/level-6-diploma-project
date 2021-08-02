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
    public class PostRepository : BaseRepository<Accommodation>, IPostRepository
    {
        public PostRepository(PostDbContext context):base(context)
        {
        }
        public async Task<ICollection<Accommodation>> GetAllAsync()
        {
            return await GetIncludes().ToListAsync();
        }

        public async Task<Accommodation> GetByIdAsync(long id)
        {
            return await GetIncludes().SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<ICollection<Accommodation>> GetFilteredAsync(Expression<Func<Accommodation, bool>> filter)
        {
            return await GetIncludes().Where(filter).ToListAsync();
        }

        private IQueryable<Accommodation> GetIncludes()
        {
           return _dbSet.Include(x => x.Owner).Include(x => x.Category)
                .Include(x => x.AccommodationPhotos)
                .Include(x => x.AccommodationRules).ThenInclude(r => r.Item)
                .Include(x => x.AccommodationFacilities).ThenInclude(f => f.Item)
                .Include(x => x.AccommodationSpecificities).ThenInclude(s => s.Item);
        }

    }
}
