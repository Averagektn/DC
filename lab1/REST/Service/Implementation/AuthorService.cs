using AutoMapper;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Interface;
using REST.Storage.Common;

namespace REST.Service.Implementation
{
    public class AuthorService(IServiceProvider serviceProvider, IMapper mapper) : IAuthorService
    {
        private readonly DbStorage _context = serviceProvider.GetRequiredService<DbStorage>();
        private readonly IMapper _mapper = mapper;

        public bool AddAuthor(AuthorRequestTO author)
        {
            var a = _mapper.Map<Author>(author);

            if (!Validate(a))
            {
                return false;
            }

            try
            {
                _context.Add(a);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public IList<AuthorResponseTO> GetAuthors()
        {
            var res = new List<AuthorResponseTO>();

            foreach (var author in _context.Authors)
            {
                res.Add(_mapper.Map<AuthorResponseTO>(author));
            }

            return res;
        }

        public bool RemoveAuthor(AuthorRequestTO author)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAuthor(AuthorRequestTO author)
        {
            throw new NotImplementedException();
        }

        protected bool Validate(Author author)
        {
            var fnameLen = author.FirstName.Length;
            var lnameLen = author.LastName.Length;
            var passLen = author.Password.Length;
            var loginLen = author.Login.Length;

            if (fnameLen < 2 || fnameLen > 64)
            {
                return false;
            }
            if (lnameLen < 2 || fnameLen > 64)
            {
                return false;
            }
            if (passLen < 8 || passLen > 128)
            {
                return false;
            }
            if (loginLen < 2 || loginLen > 64)
            {
                return false;
            }
            return true;
        }
    }
}
