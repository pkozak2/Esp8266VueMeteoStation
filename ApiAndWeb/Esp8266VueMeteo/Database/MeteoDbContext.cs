using Esp8266VueMeteo.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Esp8266VueMeteo.Database
{
    public class MeteoDbContext : DbContext
    {
        public MeteoDbContext(DbContextOptions<MeteoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Devices> Devices { get; set; }
    }
}
