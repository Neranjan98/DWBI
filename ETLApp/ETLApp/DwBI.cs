using FunctionApp1.DataAccess;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ETLApp
{
    public class DwBI
    {
        private readonly ILogger _logger;

        public DwBI(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DwBI>();
        }

        [Function("DwBI")]
        public void Run([TimerTrigger("59 23 * * 1-5")] TimerInfo myTimer)
        {

            _logger.LogInformation($"Timer Trigger Azure Function Initiated at {DateTime.Now}");

            using (var context = new VehicleContext())
            {

                _logger.LogInformation("Connected to Database to retrieve records.");

                try
                {

                    var vehicles = context
                        .Vehicles
                        .Where(x => 
                            String.IsNullOrEmpty(x.After2020))
                        .ToList();

                    if (vehicles.Count > 0)
                    {

                        _logger.LogInformation($"Number of Vehicles that need to be updated is {vehicles.Count}");

                        vehicles
                        .ForEach(x => {
                            x.After2020 =
                            x.model_year > 2020 ?
                            "Y" : "N";
                        });

                        context.SaveChanges();

                        _logger.LogInformation("Db Updated");
                    }
                    else
                    {
                        _logger.LogInformation("Database already updated.");
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occured with message: {ex.Message}");
                }
            }
        }
    }
}
