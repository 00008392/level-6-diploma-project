using Microsoft.EntityFrameworkCore;
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
        public DbSet<User> Users { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Specificity> Specificities { get; set; }
        public DbSet<DatesBooked> DatesBooked { get; set; }
        public DbSet<Feedback<User>> UserFeedbacks { get; set; }
        public DbSet<Feedback<Accommodation>> AccommodationFeedbacks { get; set; }

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
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DatesBookedEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserFeedbackEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AccommodationFeedbackEntityTypeConfiguration());
        }
    }
}
