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
    public class PostSpecificityEntityTypeConfiguration : PostItemEntityTypeConfiguration<PostSpecificity, Specificity>
    {
        public override void Configure(EntityTypeBuilder<PostSpecificity> builder)
        {
            base.Configure(builder);
            builder.HasOne(i => i.Post).WithMany(a => a.Specificities)
             .HasForeignKey(i => i.PostId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
