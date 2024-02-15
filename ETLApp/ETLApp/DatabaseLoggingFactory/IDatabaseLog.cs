namespace ETLApp.LoggingFactory
{
    public interface IDatabaseLog
    {
        void Log(string message);

        void Dispose();
    }
}