using VirtualizationService.Persistence;

namespace VirtualizationService.Services
{
    public interface IDataVirtualizationService
    {
        Task<List<Connection>> GetAllConnections(CancellationToken cancellationToken = default);
        Task<Connection?> GetAllConnectionByType(string Type, CancellationToken cancellationToken = default);
    }
}