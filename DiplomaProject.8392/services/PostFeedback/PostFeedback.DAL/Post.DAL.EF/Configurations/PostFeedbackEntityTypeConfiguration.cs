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
    public class PostFeedbackEntityTypeConfiguration :
         IEntityTypeConfiguration<Feedback<Post>>
    {
        //configuration for feedback on accommodation specified in post
        public void Configure(EntityTypeBuilder<Feedback<Post>> builder)
        {
            builder.HasKey(x => x.Id);
            //when post is deleted, related feedbacks are deleted
            builder.HasOne(x => x.Item).WithMany(x => x.Feedbacks)
                .HasForeignKey(x => x.ItemId).OnDelete(DeleteBehavior.Cascade);
            //relationship between feedback and creator is not required
            //after user deletion feedback is not removed, so creator id is set to null
            builder.HasOne(x => x.Creator).WithMany(x => x.FeedbacksForAccommodations)
                .HasForeignKey(x => x.CreatorId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
