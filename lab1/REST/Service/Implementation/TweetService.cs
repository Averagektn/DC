using REST.Service.Interface;
using REST.Storage.Common;

namespace REST.Service.Implementation
{
    public class TweetService(IServiceProvider serviceProvider) : ITweetService
    {
        private readonly DbStorage _context = serviceProvider.GetRequiredService<DbStorage>();
    }
}
