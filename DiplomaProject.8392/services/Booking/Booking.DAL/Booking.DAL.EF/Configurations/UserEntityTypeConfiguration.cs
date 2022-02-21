using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.EF.Configurations
{
    public class UserEntityTypeConfiguration :
          IEntityTypeConfiguration<User>
    {
        //configuration for user entity
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            //disable auto increment of PK, since it will be received from integration event
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
