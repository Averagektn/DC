using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Interface;
using REST.Storage.Common;

namespace REST.Service.Implementation
{
    public class AuthorService(DbStorage dbStorage, IMapper mapper) : IAuthorService
    {
        private readonly DbStorage _context = dbStorage;
        private readonly IMapper _mapper = mapper;

        public async Task<AuthorResponseTO> Add(AuthorRequestTO author)
        {
            var a = _mapper.Map<Author>(author);

            if (!Validate(a))
            {
                throw new InvalidDataException("Author is not valid");
            }

            try
            {
                _context.Add(a);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

            return _mapper.Map<AuthorResponseTO>(a);
        }

        public IList<AuthorResponseTO> GetAll()
        {
            return _context.Authors.Select(_mapper.Map<AuthorResponseTO>).ToList();
        }

        public async Task<bool> Patch(int id, JsonPatchDocument<Author> patch)
        {
            var author = await _context.FindAsync<Author>(id);

            if (author is null)
            {
                return false;
            }

            try
            {
                patch.ApplyTo(author);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Remove(int id)
        {
            var a = new Author() { Id = id };

            try
            {
                _context.Remove(a);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Update(AuthorRequestTO author)
        {
            var a = _mapper.Map<Author>(author);

            if (!Validate(a))
            {
                return false;
            }

            try
            {
                _context.Update(a);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<AuthorResponseTO> GetByID([FromRoute] int id)
        {
            var a = await _context.Authors.FindAsync(id);

            if (a is null)
            {
                throw new ArgumentNullException($"Not found AUTHOR {id}");
            }

            return _mapper.Map<AuthorResponseTO>(a);
        }

        private static bool Validate(Author author)
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
