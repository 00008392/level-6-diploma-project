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
        //configuration for feedback on user
        public void Configure(EntityTypeBuilder<Feedback<User>> builder)
        {
            builder.HasKey(x => x.Id);
            //feedback should be deleted if user on whom it is left is deleted
            //but here restrict delete behavior is specified to avoid multiple cascade paths
            //required delete behavior is achieved through trigger
            builder.HasOne(x => x.Item).WithMany(x => x.Feedbacks)
                 .HasForeignKey(x => x.ItemId).OnDelete(DeleteBehavior.Restrict);
            //relationship between feedback and creator is not required
            //after user deletion feedback is not removed, so creator id is set to null
            builder.HasOne(x => x.Creator).WithMany(x => x.FeedbacksForUsers)
                .HasForeignKey(x => x.CreatorId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
