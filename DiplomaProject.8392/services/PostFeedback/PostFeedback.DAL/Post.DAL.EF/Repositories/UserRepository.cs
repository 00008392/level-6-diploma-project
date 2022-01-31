
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
    public class UserRepository :  GenericRepositoryWithIncludes<User>
    {
       
        public UserRepository(PostDbContext context):base(context)
        {
        }
       
        public override IQueryable<User> GetDbSetWithRelatedTables()
        {
            return _dbSet.Include(x => x.Posts).ThenInclude(x => x.Bookings)
                         .Include(x => x.Bookings).ThenInclude(x => x.Post);
        }

    }
}
