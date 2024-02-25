using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using VirtualizationService.Persistence;

namespace VirtualizationService.DatabaseInfrastucture
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using DatabaseContext dbContext = 
                scope
                .ServiceProvider
                .GetRequiredService<DatabaseContext>();

            dbContext.Database.Migrate();
        }
    }
}
