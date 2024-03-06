using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Interface;
using REST.Storage.Common;

namespace REST.Service.Implementation
{
    public class PostService(IServiceProvider serviceProvider, IMapper mapper) : IPostService
    {
        private readonly DbStorage _context = serviceProvider.GetRequiredService<DbStorage>();
        private readonly IMapper _mapper = mapper;

        public Task<bool> Add(PostRequestTO author)
        {
            throw new NotImplementedException();
        }

        public IList<PostResponseTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Patch(int id, JsonPatchDocument<Post> patch)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PostRequestTO author)
        {
            throw new NotImplementedException();
        }
    }
}
