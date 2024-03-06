using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Interface;
using REST.Storage.Common;

namespace REST.Service.Implementation
{
    public class TweetService(IServiceProvider serviceProvider, IMapper mapper) : ITweetService
    {
        private readonly DbStorage _context = serviceProvider.GetRequiredService<DbStorage>();
        private readonly IMapper _mapper = mapper;

        public Task<bool> Add(TweetRequestTO author)
        {
            throw new NotImplementedException();
        }

        public IList<TweetResponseTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Patch(int id, JsonPatchDocument<Tweet> patch)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(TweetRequestTO author)
        {
            throw new NotImplementedException();
        }
    }
}
