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
    public class AccommodationPhotoEntityTypeConfiguration : IEntityTypeConfiguration<AccommodationPhoto>
    {
        public void Configure(EntityTypeBuilder<AccommodationPhoto> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasOne(i => i.Accommodation).WithMany(a => a.AccommodationPhotos)
                .HasForeignKey(i => i.AccommodationId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(i => i.Photo).IsRequired(true);
            builder.Property(i => i.MimeType).IsRequired(true);
        }
    }
}
