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
    public class AccommodationItemEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> 
        where T: ItemAccommodationBase
    {

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasOne(i => i.Item).WithMany("AccommodationItems")
                .HasForeignKey(i => i.ItemId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
