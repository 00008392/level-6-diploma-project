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
    class CityEntityTypeConfiguration: IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Country).WithMany(c => c.Cities).HasForeignKey(c => c.CountryId);
            builder.Property(c => c.Name).IsRequired(true);
        }
    }
}
