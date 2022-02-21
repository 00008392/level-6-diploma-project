using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.EF.Configurations
{
    public class PostEntityTypeConfiguration :
        IEntityTypeConfiguration<Post>
    {
        //configuraiton for post entity
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            //disable auto increment of PK, since it will be received from integration event
            builder.Property(x => x.Id).ValueGeneratedNever();
            //to avoid multiple cascade delete paths, required delete behavior is achieved through trigger
            builder.HasOne(x => x.Owner).WithMany(x => x.Posts)
                .HasForeignKey(x => x.OwnerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
