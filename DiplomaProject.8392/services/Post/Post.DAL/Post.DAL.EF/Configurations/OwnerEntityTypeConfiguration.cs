﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.DAL.EF.Configurations
{
    public class OwnerEntityTypeConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Email).IsUnique(true);
            builder.Property(u => u.Email).IsRequired(true);
            builder.Property(u => u.DateOfBirth).IsRequired(false);
            builder.Property(u => u.Gender).IsRequired(false);

        }
    }
}