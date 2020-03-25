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
            modelBuilder.Entity<Devices>().Property(x => x.DeviceId).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Measurements>().Property(x => x.MeasurementId).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<JsonUpdates>().Property(x => x.JsonUpdateId).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Devices>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Devices>().Property(x => x.LocationProvided).HasDefaultValue(false);
            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<Aggregates> Aggregates { get; set;t }
        public DbSet<Devices> Devices { get; set; }
        public DbSet<Measurements> Measurements { get; set; }
        public DbSet<JsonUpdates> JsonUpdates { get; set; }
    }
}
