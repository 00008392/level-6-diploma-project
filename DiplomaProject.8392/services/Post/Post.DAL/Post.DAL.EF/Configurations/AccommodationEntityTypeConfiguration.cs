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
    public class AccommodationEntityTypeConfiguration : IEntityTypeConfiguration<Accommodation>
    {
        public void Configure(EntityTypeBuilder<Accommodation> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Title).IsRequired(true);
            builder.HasOne(a => a.Owner).WithMany(o => o.Accommodations)
                .HasForeignKey(a => a.OwnerId).OnDelete(DeleteBehavior.Cascade);   
            builder.HasOne(a => a.Category).WithMany(c => c.Accommodations)
                .HasForeignKey(a => a.CategoryId).OnDelete(DeleteBehavior.SetNull);
            builder.Property(a => a.Address).IsRequired(true);
            builder.Property(a => a.ContactNumber).IsRequired(true);
            builder.Property(a => a.MovingInTime).HasColumnType("time").HasConversion<TimeSpan>(t=>t.TimeOfDay, t=>DateTime.Now.Add(t));
            builder.Property(a => a.MovingOutTime).HasColumnType("time").HasConversion<TimeSpan>(t => t.TimeOfDay, t => DateTime.Now.Add(t));
        }
    }
}
