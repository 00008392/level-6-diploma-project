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
            builder.HasKey(x => x.Id);
            //when post is deleted, all photos related to it are deleted
            builder.HasOne(x => x.Post).WithMany(x => x.Photos)
                .HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.PhotoBytes).IsRequired(true);
        }
    }
}
