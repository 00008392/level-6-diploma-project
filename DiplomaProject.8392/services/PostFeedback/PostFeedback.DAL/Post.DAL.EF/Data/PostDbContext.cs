using Microsoft.EntityFrameworkCore;
using PostFeedback.DAL.EF.Configurations;
using PostFeedback.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.DAL.EF.Data
{
    public class PostDbContext: DbContext
    {
        public PostDbContext(DbContextOptions<PostDbContext> options)
           : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostFacility> PostFacilities { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PostRule> PostRules { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Feedback<User>> UserFeedbacks { get; set; }
        public DbSet<Feedback<Post>> PostFeedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //apply configurations for entities for correct DB creation
            //ignore base classes
            modelBuilder.Ignore<Item>();
            modelBuilder.Ignore<PostItem>();
            modelBuilder.ApplyConfiguration(new PostEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PhotoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PostFacilityEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PostRuleEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new PostSpecificityEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new CityEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemEntityTypeConfiguration<Rule>());
            modelBuilder.ApplyConfiguration(new ItemEntityTypeConfiguration<Facility>());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BookingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserFeedbackEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PostFeedbackEntityTypeConfiguration());
        }
    }
}
