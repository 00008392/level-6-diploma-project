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
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        //configuration for user
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            //disable auto increment of PK, since it will be retrieved from integration event
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasIndex(u => u.Email).IsUnique(true);
            builder.Property(u => u.Email).IsRequired(true);
            builder.Property(u => u.FirstName).IsRequired(true);
            builder.Property(u => u.LastName).IsRequired(true);
        }
    }
}
