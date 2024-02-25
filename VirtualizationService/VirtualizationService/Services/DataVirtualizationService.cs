using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VirtualizationService.Factories.Base;
using VirtualizationService.Persistence;
using VirtualizationService.ContractExtensions;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace VirtualizationService.Services
{
    public class DataVirtualizationService : IDataVirtualizationService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger _logger;
        private readonly IDistributedCache _distributedCache;

        public DataVirtualizationService(IFactory factory, 
            ILoggerFactory logFactory,
            IDistributedCache distributedCache)
        {
            _databaseContext = factory.CreateInstance();
            _logger = logFactory.CreateLogger<DataVirtualizationService>();
            _distributedCache = distributedCache;
        }

        public async Task<Connection?> GetAllConnectionByType(string type, CancellationToken cancellationToken = default)
        {
            try
            {

                var cachedResult = await _distributedCache.GetStringAsync(type, cancellationToken);

                if (!String.IsNullOrEmpty(cachedResult))
                {
                    return JsonConvert.DeserializeObject<Connection>(cachedResult);
                }
                else
                {
                    using (_databaseContext)
                    {
                        var connectionResult = await _databaseContext.Connections
                            .Where(x => x.ConnectionType == type)
                            .FirstAsync(cancellationToken);

                        var transformedResult = connectionResult.ConvertConnectionStringToBase64();

                        _ = _distributedCache
                            .SetStringAsync(type, 
                            JsonConvert.SerializeObject(transformedResult), 
                            cancellationToken);

                        return transformedResult;

                    }
                }  
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occured {ex.Message}");
                throw;
            }
        }

        public async Task<List<Connection>> GetAllConnections(CancellationToken cancellationToken = default)
        {
            try
            {
                using (_databaseContext)
                {
                    var connectionResult = await _databaseContext
                        .Connections
                        .ToListAsync(cancellationToken);

                    var transformedResult = new List<Connection>();

                    connectionResult.ForEach(connection =>
                    {
                        var interimResult = connection.ConvertConnectionStringToBase64();
                        transformedResult.Add(interimResult);
                    });

                    return transformedResult;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occured {ex.Message}");
                throw;
            }
        }

    }
}
