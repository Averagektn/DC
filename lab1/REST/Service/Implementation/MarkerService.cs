using REST.Service.Interface;
using REST.Storage.Common;

namespace REST.Service.Implementation
{
    public class MarkerService(IServiceProvider serviceProvider) : IMarkerService
    {
        private readonly DbStorage _context = serviceProvider.GetRequiredService<DbStorage>();
    }
}
