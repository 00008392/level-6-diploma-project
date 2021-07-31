using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.DAL.EF.Configurations
{
    public class AccommodationItemEntityTypeConfiguration<T, E> : IEntityTypeConfiguration<T> 
        where T: ItemAccommodationBase
        where E: ItemBase
    {

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasOne(i => (E)i.Item).WithMany("AccommodationItems")
                .HasForeignKey(i => i.ItemId).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(i => new { i.ItemId, i.AccommodationId, i.OtherItem })
                .IsUnique(true);
            builder.HasIndex(i => new { i.ItemId, i.AccommodationId })
                .IsUnique(true)
                .HasFilter("OtherItem is null");
        }
    }
}
