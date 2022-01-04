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
    public class AccountRepository : GenericRepositoryWithIncludes<User>
    {
        public AccountRepository(AccountDbContext context)
            : base(context)
        {

        }
        public override IQueryable<User> GetDbSetWithRelatedTables()
        {
            return _dbSet.Include(x => x.BookingsAsGuest)
                         .Include(x=>x.BookingsAsOwner)
                         .Include(x => x.City).ThenInclude(x => x == null ? null : x.Country);
        }

       
    }
}
