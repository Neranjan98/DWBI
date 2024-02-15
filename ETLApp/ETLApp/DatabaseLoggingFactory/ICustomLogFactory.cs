using ETLApp.LoggingFactory;

namespace ETLApp.DatabaseLoggingFactory
{
    public interface ICustomLogFactory
    {
        DatabaseLoggingService CreateDatabaseLoggingService();
    }
}