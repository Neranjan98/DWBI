using ETLApp.Enums;
using ETLApp.LoggingFactory;
using FunctionApp1.DataAccess;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ETLApp.DatabaseLoggingFactory;

namespace ETLApp
{
    public class DwBI
    {
        private readonly ILogger _logger;
        private readonly IDatabaseLog _databaseLoggingService;

        public DwBI(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DwBI>();
            _databaseLoggingService = new LogFactory().CreateDatabaseLoggingService();
        }

        [Function("DwBI")]
        public void Run([TimerTrigger("59 23 * * 1-5")] TimerInfo myTimer)
        {

            _logger.LogInformation($"Timer Trigger Azure Function Initiated at {DateTime.Now}");

            string status = CustomEnums.LogType.Initiated.ToString();

            _databaseLoggingService.Log(status);

            using (var context = new VehicleContext())
            {

                _logger.LogInformation("Connected to Database to retrieve records.");

                try
                {

                    context.Vehicles
                    .Where(x => String.IsNullOrEmpty(x.After2020))
                    .ExecuteUpdate(p => p.SetProperty(c => c.After2020, 
                                    r => r.model_year > 2020 ? "Y" : "N"));

                    status = CustomEnums.LogType.Success.ToString();

                    _databaseLoggingService.Log(status);

                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occured with message: {ex.Message}");

                    status = CustomEnums.LogType.Failure.ToString();
                    
                    _databaseLoggingService.Log(status);

                }
                finally
                {
                    _databaseLoggingService.Dispose();
                }
            }
        }
    }
}
