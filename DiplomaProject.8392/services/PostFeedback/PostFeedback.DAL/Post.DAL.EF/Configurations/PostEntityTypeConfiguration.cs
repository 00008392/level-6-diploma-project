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
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Title).IsRequired(true);
            //post should be deleted if its owner is deleted
            //but here restrict delete behavior is specified to avoid multiple cascade paths
            //required delete behavior is achieved through trigger on Users table
            //which deletes post and then user
            builder.HasOne(a => a.Owner).WithMany(o => o.Posts)
                .HasForeignKey(a => a.OwnerId).OnDelete(DeleteBehavior.Restrict);   
            builder.HasOne(a => a.Category).WithMany(c => c.Posts)
                .HasForeignKey(a => a.CategoryId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(a => a.City).WithMany(c => c.Posts)
               .HasForeignKey(a => a.CityId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(a => a.Address).IsRequired(true);
            builder.Property(a => a.ContactNumber).IsRequired(true);
            //store moving in/out time in time format and retrieve as string
            builder.Property(a => a.MovingInTime).HasColumnType("time")
                .HasConversion(t=>TimeSpan.Parse(t), t=>t.ToString("hh\\:mm"));
            builder.Property(a => a.MovingOutTime).HasColumnType("time")
                .HasConversion(t => TimeSpan.Parse(t), t => t.ToString("hh\\:mm"));
        }
    }
}
