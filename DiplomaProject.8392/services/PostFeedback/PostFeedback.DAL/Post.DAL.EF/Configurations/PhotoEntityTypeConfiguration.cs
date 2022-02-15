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
    public class PhotoEntityTypeConfiguration : IEntityTypeConfiguration<Photo>
    {
        //configuration fo photo
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(i => i.Id);
            //when post is deleted, all photos related to it are deleted
            builder.HasOne(i => i.Post).WithMany(a => a.Photos)
                .HasForeignKey(i => i.PostId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(i => i.PhotoBytes).IsRequired(true);
            builder.Property(i => i.MimeType).IsRequired(true);
        }
    }
}
