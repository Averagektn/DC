﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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

        public async Task<bool> AddAuthor(AuthorRequestTO author)
        {
            var a = _mapper.Map<Author>(author);

            if (!Validate(a))
            {
                return false;
            }

            try
            {
                _context.Add(a);
                await _context.SaveChangesAsync();
            }
            catch
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

        public async Task<bool> Patch(int id, JsonPatchDocument<Author> patch)
        {
            try
            {
                var author = await _context.FindAsync<Author>(id);

                if (author is null)
                {
                    return false;
                }

                patch.ApplyTo(author);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> RemoveAuthor(int id)
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

        public async Task<bool> UpdateAuthor(AuthorRequestTO author)
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
