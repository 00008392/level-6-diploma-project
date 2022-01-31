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
    public class PostItemEntityTypeConfiguration<T, E> : IEntityTypeConfiguration<T> 
        where T: PostItem
        where E: Item
    {

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasOne(i => (E)i.Item).WithMany("PostItems")
                .HasForeignKey(i => i.ItemId).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(i => new { i.ItemId, i.PostId, i.OtherValue })
                .IsUnique(true);
            builder.HasIndex(i => new { i.ItemId, i.PostId })
                .IsUnique(true)
                .HasFilter("OtherValue is null");
        }
    }
}
