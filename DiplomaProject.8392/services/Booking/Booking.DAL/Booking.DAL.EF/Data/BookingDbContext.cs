using Booking.DAL.EF.Configurations;
using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.EF.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options)
          : base(options)
        {
        }
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<BookingRequest> BookingRequests { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BookingRequestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AccommodationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CoTravelBookingEntityTypeConfiguration());
        }
    }
}
