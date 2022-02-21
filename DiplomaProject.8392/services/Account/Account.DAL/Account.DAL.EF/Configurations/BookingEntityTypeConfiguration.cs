using Account.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.DAL.EF.Configurations
{
   public class BookingEntityTypeConfiguration: IEntityTypeConfiguration<Booking>
    {
        //configuration for booking
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.Id);
            //disable auto increment of PK, since it will be received from integration event
            builder.Property(x => x.Id).ValueGeneratedNever();
            //if user has active bookings as guest (end date > datetime.now,
            //account cannot be deleted
            //required delete behavior is enabled through business logic and through trigger
            builder.HasOne(x => x.Guest).WithMany(x => x.BookingsAsGuest)
                .HasForeignKey(x => x.GuestId).OnDelete(DeleteBehavior.Restrict);
            //if owner has active bookings (end date > datetime.now,
            //account cannot be deleted
            //required delete behavior is enabled through business logic and through trigger
            builder.HasOne(x => x.Owner).WithMany(x => x.BookingsAsOwner)
              .HasForeignKey(x => x.OwnerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
