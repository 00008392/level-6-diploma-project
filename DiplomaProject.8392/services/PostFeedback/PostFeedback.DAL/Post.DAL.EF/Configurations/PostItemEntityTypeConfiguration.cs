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
    //configuration for category, city, rule, facility
    public class PostItemEntityTypeConfiguration<T> :
        IEntityTypeConfiguration<T> where T: Item
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired(true);
        }
    }
}
