
using BaseClasses.Repositories.EF;
using Microsoft.EntityFrameworkCore;
using Profile.DAL.EF.Data;
using Profile.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.DAL.EF.Repositories
{
    public class ProfileRepository : GenericRepositoryWithIncludes<User>
    {
        public ProfileRepository(ProfileDbContext context)
            : base(context)
        {

        }
        public override IQueryable<User> GetDbSetWithRelatedTables()
        {
            return _dbSet.Include(x => x.City).ThenInclude(x => x.Country);
        }
    }
}
