using REST.Service.Interface;
using REST.Storage;

namespace REST.Service.Implementation
{
    public class MarkerService : IMarkerService
    {
        private readonly DbStorage _context;

        public MarkerService(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<DbStorage>();
        }
    }
}
