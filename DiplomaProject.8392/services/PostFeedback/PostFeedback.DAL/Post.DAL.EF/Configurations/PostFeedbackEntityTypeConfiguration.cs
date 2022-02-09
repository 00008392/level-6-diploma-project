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
         IEntityTypeConfiguration<Feedback<Domain.Entities.Post>>
    {
        public void Configure(EntityTypeBuilder<Feedback<Domain.Entities.Post>> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Item).WithMany(x => x.Feedbacks)
                .HasForeignKey(x => x.ItemId).OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(x => x.FeedbackOwner).WithMany(x => x.FeedbacksForAccommodations)
            //    .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
