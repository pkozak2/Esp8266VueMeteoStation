using Esp8266VueMeteo.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Esp8266VueMeteo.Database
{
    public class MeteoDbContext : DbContext
    {
        public MeteoDbContext(DbContextOptions<MeteoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().Property(x => x.UserId).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Users>().Property(x => x.InsertDate).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Users>().HasIndex(u => u.Username).IsUnique();


            modelBuilder.Entity<Devices>().Property(x => x.DeviceId).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Devices>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Devices>().Property(x => x.LocationProvided).HasDefaultValue(false);
            modelBuilder.Entity<Devices>().Property(x => x.DeviceNormalizedName).HasDefaultValue("");


            modelBuilder.Entity<Measurements>().Property(x => x.MeasurementId).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<JsonUpdates>().Property(x => x.JsonUpdateId).HasDefaultValueSql("NEWID()");

            
            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<Devices> Devices { get; set; }
        public DbSet<Measurements> Measurements { get; set; }
        public DbSet<JsonUpdates> JsonUpdates { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
