using Microsoft.EntityFrameworkCore;
using VirtualizationService.Persistence;

namespace VirtualizationService.Factories.Base
{
    public interface IFactory : IDisposable
    {
        DatabaseContext CreateInstance();

    }
}