using REST.Service.Interface;
using REST.Storage;

namespace REST.Service.Implementation
{
    public class TweetService : ITweetService
    {
        private readonly DbStorage _context;

        public TweetService(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<DbStorage>();
        }
    }
}
