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
    class CoTravelBookingEntityTypeConfiguration : IEntityTypeConfiguration<CoTravelerBooking>
    {
        public void Configure(EntityTypeBuilder<CoTravelerBooking> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.CoTraveler).WithMany(x => x.BookingRequestsAsCoTraveler)
                .HasForeignKey(x => x.CoTravelerId);
            builder.HasOne(x => x.Booking).WithMany(x => x.CoTravelers)
                .HasForeignKey(x => x.BookingId);
        }
    }
}
