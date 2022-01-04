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
    public class DatesBookedEntityTypeConfiguration : IEntityTypeConfiguration<DatesBooked>
    {
        public void Configure(EntityTypeBuilder<DatesBooked> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasOne(x => x.Accommodation).WithMany(x => x.DatesBooked)
                .HasForeignKey(x => x.AccommodationId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
