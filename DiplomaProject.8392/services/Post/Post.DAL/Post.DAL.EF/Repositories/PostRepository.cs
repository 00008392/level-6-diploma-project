
using BaseClasses.Repositories.EF;
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
    public class PostRepository :  GenericRepositoryWithIncludes<Accommodation>
    {
       
        public PostRepository(PostDbContext context):base(context)
        {
        }
       
        public override IQueryable<Accommodation> GetDbSetWithRelatedTables()
        {
            return _dbSet.Include(x => x.Owner).Include(x => x.Category)
                 .Include(x => x.AccommodationPhotos)
                 .Include(x => x.AccommodationRules).ThenInclude(r => r.Item)
                 .Include(x => x.AccommodationFacilities).ThenInclude(f => f.Item)
                 .Include(x => x.AccommodationSpecificities).ThenInclude(s => s.Item)
                 .Include(x => x.DatesBooked);
        }

    }
}
