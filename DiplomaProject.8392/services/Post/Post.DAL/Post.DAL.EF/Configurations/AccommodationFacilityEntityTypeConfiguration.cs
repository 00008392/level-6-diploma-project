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
    public class AccommodationFacilityEntityTypeConfiguration : IEntityTypeConfiguration<AccommodationFacility>
    {
        public void Configure(EntityTypeBuilder<AccommodationFacility> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasOne(i => i.Accommodation).WithMany(a => a.AccommodationFacilities)
                .HasForeignKey(i => i.AccommodationId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(i => i.Facility).WithMany(r => r.AccommodationFacilities)
                .HasForeignKey(i => i.ItemId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
