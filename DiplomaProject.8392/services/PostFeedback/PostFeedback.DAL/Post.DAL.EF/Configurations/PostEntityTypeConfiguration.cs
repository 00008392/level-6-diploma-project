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
    public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
    {
        //configuration for post
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired(true);
            //post should be deleted if its owner is deleted
            //but here restrict delete behavior is specified to avoid multiple cascade paths
            //and because post cannot be deleted if there are active bookings on accommodation specified in this post
            //required delete behavior is achieved through trigger on Users table
            //which deletes post and then user
            builder.HasOne(x => x.Owner).WithMany(x => x.Posts)
                .HasForeignKey(x => x.OwnerId).OnDelete(DeleteBehavior.Restrict);   
            //not required relationship, so can be set to null
            builder.HasOne(x => x.Category).WithMany(x => x.Posts)
                .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.City).WithMany(x => x.Posts)
               .HasForeignKey(x => x.CityId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.Address).IsRequired(true);
            builder.Property(x => x.ContactNumber).IsRequired(true);
            //store moving in/out time in time format and retrieve as string
            builder.Property(x => x.MovingInTime).HasColumnType("time")
                .HasConversion(t => TimeSpan.Parse(t), t => t.ToString("hh\\:mm")).IsRequired(true);
            builder.Property(x => x.MovingOutTime).HasColumnType("time")
                .HasConversion(t => TimeSpan.Parse(t), t => t.ToString("hh\\:mm")).IsRequired(true);
        }
    }
}
