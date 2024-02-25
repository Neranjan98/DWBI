using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VirtualizationService.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Connection> Connections { get; set; }

        private readonly IConfiguration _configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("BaseDb");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Connection>()
                .HasKey(x => x.ConnectionType);
                
        }

    }
}
