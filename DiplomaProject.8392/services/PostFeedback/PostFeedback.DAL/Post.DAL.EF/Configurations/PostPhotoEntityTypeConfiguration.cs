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
    public class PostPhotoEntityTypeConfiguration : IEntityTypeConfiguration<PostPhoto>
    {
        public void Configure(EntityTypeBuilder<PostPhoto> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasOne(i => i.Post).WithMany(a => a.Photos)
                .HasForeignKey(i => i.PostId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(i => i.Photo).IsRequired(true);
            builder.Property(i => i.MimeType).IsRequired(true);
        }
    }
}
