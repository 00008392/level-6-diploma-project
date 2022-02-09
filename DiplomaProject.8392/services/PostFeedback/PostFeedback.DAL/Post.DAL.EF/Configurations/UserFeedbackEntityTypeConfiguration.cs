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
    class UserFeedbackEntityTypeConfiguration : IEntityTypeConfiguration<Feedback<User>>
    {
        public void Configure(EntityTypeBuilder<Feedback<User>> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Item).WithMany(x => x.Feedbacks)
                 .HasForeignKey(x => x.ItemId).OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne(x => x.FeedbackOwner).WithMany(x => x.FeedbacksForUsers)
            //    .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
