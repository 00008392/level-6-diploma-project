using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Domain.Core;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.DAL.EF.Configurations
{
    public class AccommodationRuleEntityTypeConfiguration : AccommodationItemEntityTypeConfiguration<AccommodationRule, Rule>
    {
        public override void Configure(EntityTypeBuilder<AccommodationRule> builder)
        {
            base.Configure(builder);
            builder.HasOne(i => i.Accommodation).WithMany(a => a.AccommodationRules)
               .HasForeignKey(i => i.AccommodationId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
