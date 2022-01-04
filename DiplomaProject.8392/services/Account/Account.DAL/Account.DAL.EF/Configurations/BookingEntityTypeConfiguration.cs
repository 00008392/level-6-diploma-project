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
    public class BookingEntityTypeConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasOne(x => x.Guest).WithMany(x => x.BookingsAsGuest)
                .HasForeignKey(x => x.GuestId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Owner).WithMany(x => x.BookingsAsOwner)
                .HasForeignKey(x => x.OwnerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
