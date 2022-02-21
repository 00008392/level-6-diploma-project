using Account.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.DAL.EF.Configurations
{
    //configuration for user
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            //email is unique for each user, since it is a part of user credentials
            builder.HasIndex(x => x.Email).IsUnique(true);
            builder.Property(x => x.Email).IsRequired(true);
            builder.Property(x => x.FirstName).IsRequired(true);
            builder.Property(x => x.LastName).IsRequired(true);
            builder.Property(x => x.PasswordSalt).IsRequired(true);
            builder.Property(x => x.PasswordHash).IsRequired(true);
            builder.HasOne(x => x.Country).WithMany(x => x.Users).
                HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.ProfilePhoto).IsRequired(false);
        }
    }
}
