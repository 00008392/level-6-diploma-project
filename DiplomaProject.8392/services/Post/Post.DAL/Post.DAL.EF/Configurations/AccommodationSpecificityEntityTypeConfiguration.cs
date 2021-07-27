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
    public class AccommodationSpecificityEntityTypeConfiguration : IEntityTypeConfiguration<AccommodationSpecificity>
    {
        public void Configure(EntityTypeBuilder<AccommodationSpecificity> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasOne(i => i.Accommodation).WithMany(a => a.AccommodationSpecificities)
                .HasForeignKey(i => i.AccommodationId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(i => i.Specificity).WithMany(r => r.AccommodationSpecificities)
                .HasForeignKey(i => i.ItemId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
