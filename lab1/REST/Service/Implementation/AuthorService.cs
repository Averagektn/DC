using AutoMapper;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Interface;
using REST.Storage.Common;

namespace REST.Service.Implementation
{
    public class AuthorService(IServiceProvider serviceProvider, IMapper mapper) : IAuthorService
    {
        private readonly DbStorage _context = serviceProvider.GetRequiredService<DbStorage>();
        private readonly IMapper _mapper = mapper;

        public IList<AuthorResponseTO> GetAuthors()
        {
            var res = new List<AuthorResponseTO>();

            foreach (var author in _context.Authors)
            {
                res.Add(_mapper.Map<AuthorResponseTO>(author));
            }

            return res;
        }
    }
}
