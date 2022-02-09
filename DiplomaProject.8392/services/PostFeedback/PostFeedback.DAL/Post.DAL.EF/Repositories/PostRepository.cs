using BaseClasses.Repositories.EF;
using Microsoft.EntityFrameworkCore;
using PostFeedback.DAL.EF.Data;
using PostFeedback.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.DAL.EF.Repositories
{
    //public class PostRepository :  GenericRepositoryWithIncludes<Post>
    //{
       
    //    public PostRepository(PostDbContext context):base(context)
    //    {
    //    }
       
    //    public override IQueryable<Post> GetDbSetWithRelatedTables()
    //    {
    //        return _dbSet.Include(x => x.Owner).Include(x => x.Category)
    //             .Include(x => x.City)
    //             .Include(x => x.Photos)
    //             .Include(x => x.Rules).ThenInclude(r => r.Item)
    //             .Include(x => x.Facilities).ThenInclude(f => f.Item)
    //             .Include(x => x.Specificities).ThenInclude(s => s.Item)
    //             .Include(x => x.Bookings)
    //             .AsSplitQuery()
    //             .OrderByDescending(x => x.DatePublished);
    //    }

    //}
}
