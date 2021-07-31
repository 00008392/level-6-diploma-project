using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.DAL.EF.Configurations
{
    public class AccommodationFacilityEntityTypeConfiguration : AccommodationItemEntityTypeConfiguration<AccommodationFacility, Facility>
    {
        public override void Configure(EntityTypeBuilder<AccommodationFacility> builder)
        {
            base.Configure(builder);
            builder.HasOne(i => i.Accommodation).WithMany(a => a.AccommodationFacilities)
                .HasForeignKey(i => i.AccommodationId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
