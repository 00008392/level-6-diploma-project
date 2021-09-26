using BaseClasses.Repositories.EF;
using Booking.DAL.EF.Data;
using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.EF.Repositories
{
    public class BookingRepository : GenericRepositoryWithIncludes<BookingRequest>
    {
        public BookingRepository(BookingDbContext context)
            :base(context)
        {

        }
        public override IQueryable<BookingRequest> GetDbSetWithRelatedTables()
        {
            return _dbSet.Include(x => x.Guest)
                         .Include(x => x.Accommodation);
        }
    }
}
