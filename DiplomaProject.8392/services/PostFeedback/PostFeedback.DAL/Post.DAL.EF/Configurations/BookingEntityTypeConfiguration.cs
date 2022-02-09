using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostFeedback.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.DAL.EF.Configurations
{
    public class BookingEntityTypeConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.Id);
            //disable auto increment of PK, since it will be received from integration event
            builder.Property(x => x.Id).ValueGeneratedNever();
            //if accommodation has active bookings (end date > datetime.now,
            //it cannot be deleted
            //required delete behavior is enabled through business logic and through trigger
            builder.HasOne(x => x.Post).WithMany(x => x.Bookings)
                .HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Restrict);
            //if guest has active bookings (end date > datetime.now,
            //it cannot be deleted
            //required delete behavior is enabled through business logic and through trigger
            builder.HasOne(x => x.Guest).WithMany(x => x.Bookings)
              .HasForeignKey(x => x.GuestId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
