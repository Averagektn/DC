using REST.Service.Interface;
using REST.Storage.Common;

namespace REST.Service.Implementation
{
    public class PostService(IServiceProvider serviceProvider) : IPostService
    {
        private readonly DbStorage _context = serviceProvider.GetRequiredService<DbStorage>();
    }
}
