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
    public class AccommodationSpecificityEntityTypeConfiguration : AccommodationItemEntityTypeConfiguration<AccommodationSpecificity, Specificity>
    {
        public override void Configure(EntityTypeBuilder<AccommodationSpecificity> builder)
        {
            base.Configure(builder);
            builder.HasOne(i => i.Accommodation).WithMany(a => a.AccommodationSpecificities)
             .HasForeignKey(i => i.AccommodationId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
