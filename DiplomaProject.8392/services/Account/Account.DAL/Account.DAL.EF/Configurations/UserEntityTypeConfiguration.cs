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
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Email).IsUnique(true);
            builder.Property(u => u.Email).IsRequired(true);
            builder.Property(u => u.FirstName).IsRequired(true);
            builder.Property(u => u.LastName).IsRequired(true);
            builder.Property(u => u.Role).IsRequired(true);
            builder.Property(u => u.PasswordSalt).IsRequired(true);
            builder.Property(u => u.PasswordHash).IsRequired(true);
            builder.HasOne(u => u.Country).WithMany(c => c.Users).
                HasForeignKey(u => u.CountryId).OnDelete(DeleteBehavior.SetNull);
            builder.Property(u => u.ProfilePhoto).IsRequired(false);
        }
    }
}
