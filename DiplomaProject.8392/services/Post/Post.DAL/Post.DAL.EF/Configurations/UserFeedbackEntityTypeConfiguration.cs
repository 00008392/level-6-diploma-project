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
    class UserFeedbackEntityTypeConfiguration : IEntityTypeConfiguration<Feedback<User>>
    {
        public void Configure(EntityTypeBuilder<Feedback<User>> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Item).WithMany(x => x.Feedbacks)
                 .HasForeignKey(x => x.ItemId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.FeedbackOwner).WithMany(x => x.FeedbacksAsOwnerForUsers)
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
