using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.EF.Configurations
{
    public class BookingRequestEntityTypeConfiguration :
          IEntityTypeConfiguration<Domain.Entities.Booking>
    {
        //configuration for booking entity
        public void Configure(EntityTypeBuilder<Domain.Entities.Booking> builder)
        {
            builder.HasKey(x => x.Id);
            //to avoid multiple cascade delete paths, required delete behavior is achieved through trigger
            builder.HasOne(x => x.Guest).WithMany(x => x.Bookings)
                .HasForeignKey(x => x.GuestId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Post).WithMany(x => x.Bookings)
                .HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Restrict);
           
        }
    }
}
