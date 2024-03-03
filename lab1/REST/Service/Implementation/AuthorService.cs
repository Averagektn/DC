using REST.Service.Interface;
using REST.Storage;

namespace REST.Service.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly DbStorage _context;

        public AuthorService(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<DbStorage>();
        }
    }
}
