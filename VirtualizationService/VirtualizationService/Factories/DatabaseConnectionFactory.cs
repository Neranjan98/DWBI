using Microsoft.EntityFrameworkCore;
using VirtualizationService.Factories.Base;
using VirtualizationService.Persistence;

namespace VirtualizationService.Factories
{
    public class DatabaseConnectionFactory : IFactory
    {
        private readonly DatabaseContext _context;

        public DatabaseConnectionFactory(IConfiguration configuration)
        {
            _context = new DatabaseContext(configuration);
        }

        public DatabaseContext CreateInstance()
        {
            return _context;
        }

        public void Dispose()
        {
            _ = _context.DisposeAsync();
        }

    }
}
