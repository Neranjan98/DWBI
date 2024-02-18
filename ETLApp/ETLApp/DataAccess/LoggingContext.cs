using ETLApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ETLApp.DataAccess
{
    public class LoggingContext : DbContext
    {
        public DbSet<DatabaseLog> logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("DatabaseConnectionString");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<DatabaseLog>()
                .HasKey(e => e.LogID);
        }
    }
}
