﻿using Microsoft.EntityFrameworkCore;
using Post.DAL.EF.Configurations;
using Post.Domain.Core;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.DAL.EF.Data
{
    public class PostDbContext: DbContext
    {
        public PostDbContext(DbContextOptions<PostDbContext> options)
           : base(options)
        {
        }

        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<AccommodationFacility> AccommodationFacilities { get; set; }
        public DbSet<AccommodationPhoto> AccommodationPhotos { get; set; }
        public DbSet<AccommodationRule> AccommodationRules { get; set; }
        public DbSet<AccommodationSpecificity> AccommodationSpecificities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Specificity> Specificities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ItemBase>();
            modelBuilder.Ignore<ItemAccommodationBase>();
            modelBuilder.ApplyConfiguration(new AccommodationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AccommodationPhotoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AccommodationFacilityEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AccommodationRuleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AccommodationSpecificityEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemEntityTypeConfiguration<Rule>());
            modelBuilder.ApplyConfiguration(new ItemEntityTypeConfiguration<Facility>());
            modelBuilder.ApplyConfiguration(new ItemEntityTypeConfiguration<Specificity>());
            modelBuilder.ApplyConfiguration(new OwnerEntityTypeConfiguration());
        }
    }
}