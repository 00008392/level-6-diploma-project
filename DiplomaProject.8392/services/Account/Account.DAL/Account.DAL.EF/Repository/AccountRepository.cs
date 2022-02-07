using Account.DAL.EF.Data;
using Account.Domain.Entities;
using BaseClasses.Repositories.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.DAL.EF.Repository
{
    //this repository extends generic repository defined in BaseClasses project
    //therefore, it has all repository methods + method for including entities related to User
    public class AccountRepository : GenericRepositoryWithIncludes<User>
    {
        public AccountRepository(AccountDbContext context)
            : base(context)
        {

        }
        public override IQueryable<User> GetDbSetWithRelatedTables()
        {
            //include country entity when querying user
            return _dbSet.Include(x => x.Country);
        }

       
    }
}
