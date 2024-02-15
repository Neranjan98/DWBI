using ETLApp.DataAccess;
using ETLApp.LoggingFactory;

namespace ETLApp.DatabaseLoggingFactory
{
    public class LogFactory : ICustomLogFactory
    {
        public DatabaseLoggingService CreateDatabaseLoggingService()
        {
            return new DatabaseLoggingService(new LoggingContext());
        }

    }
}
