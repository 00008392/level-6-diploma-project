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
    public class PostRuleEntityTypeConfiguration : PostItemEntityTypeConfiguration<PostRule, Rule>
    {
        public override void Configure(EntityTypeBuilder<PostRule> builder)
        {
            base.Configure(builder);
            builder.HasOne(i => i.Post).WithMany(a => a.Rules)
               .HasForeignKey(i => i.PostId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
