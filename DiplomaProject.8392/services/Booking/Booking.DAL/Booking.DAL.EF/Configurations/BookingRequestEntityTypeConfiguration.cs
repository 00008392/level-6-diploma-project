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
          IEntityTypeConfiguration<BookingRequest>
    {
        public void Configure(EntityTypeBuilder<BookingRequest> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasOne(r => r.Guest).WithMany(g => g.BookingRequestsAsMainGuest)
                .HasForeignKey(r => r.GuestId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.Accommodation).WithMany(a => a.BookingRequests)
                .HasForeignKey(r => r.AccommodationId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(r => r.CoTravelers).WithMany(x => x.BookingRequestsAsCoTraveler);
           
        }
    }
}
