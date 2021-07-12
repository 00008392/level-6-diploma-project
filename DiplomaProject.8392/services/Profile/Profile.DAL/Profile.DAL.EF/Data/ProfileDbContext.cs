using Microsoft.EntityFrameworkCore;
using Profile.DAL.EF.Configurations;
using Profile.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.DAL.EF.Data
{
   public class ProfileDbContext: DbContext
    {
        public ProfileDbContext(DbContextOptions<ProfileDbContext> options)
          : base(options)
        {
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CityEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CountryEntityTypeConfiguration());
        }
    }
}
