using REST.Service.Interface;
using REST.Storage;

namespace REST.Service.Implementation
{
    public class PostService : IPostService
    {
        private readonly DbStorage _context;

        public PostService(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<DbStorage>();
        }
    }
}
