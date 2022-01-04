using BaseClasses.Contracts;
using BaseClasses.Repositories.EF;
using Microsoft.EntityFrameworkCore;
using Post.DAL.EF.Data;
using Post.Domain.Core;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.DAL.EF.Repositories
{
    public class FeedbackRepository<T>: GenericRepositoryWithIncludes<Feedback<T>> 
        where T: FeedbackEntity
    {
        public FeedbackRepository(PostDbContext context) : base(context)
        {
        }

        public override IQueryable<Feedback<T>> GetDbSetWithRelatedTables()
        {
            return _dbSet.Include(x => x.Item)
                         .Include(x => x.FeedbackOwner);
        }
    }
}
