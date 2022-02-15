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
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Feedback<User>> UserFeedbacks { get; set; }
        public DbSet<Feedback<Post>> PostFeedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //apply configurations for entities for correct DB creation
            //ignore base classes
            modelBuilder.Ignore<Item>();
            modelBuilder.ApplyConfiguration(new PostEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PhotoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PostItemEntityTypeConfiguration<Category>());
            modelBuilder.ApplyConfiguration(new PostItemEntityTypeConfiguration<City>());
            modelBuilder.ApplyConfiguration(new PostItemEntityTypeConfiguration<Facility>());
            modelBuilder.ApplyConfiguration(new PostItemEntityTypeConfiguration<Rule>());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BookingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserFeedbackEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PostFeedbackEntityTypeConfiguration());
        }
    }
}
