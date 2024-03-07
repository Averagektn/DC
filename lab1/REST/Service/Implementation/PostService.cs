using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Interface;
using REST.Storage.Common;

namespace REST.Service.Implementation
{
    public class PostService(DbStorage dbStorage, IMapper mapper) : IPostService
    {
        private readonly DbStorage _context = dbStorage;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> Add(PostRequestTO post)
        {
            var p = _mapper.Map<Post>(post);

            if (!Validate(p))
            {
                return false;
            }

            try
            {
                _context.Posts.Add(p);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IList<PostResponseTO> GetAll()
        {
            var res = new List<PostResponseTO>();

            foreach (var p in _context.Posts)
            {
                res.Add(_mapper.Map<PostResponseTO>(p));
            }

            return res;
        }

        public async Task<bool> Patch(int id, JsonPatchDocument<Post> patch)
        {
            var target = _context.Find<Post>(id);
            if (target is null)
            {
                return false;
            }

            try
            {
                patch.ApplyTo(target);
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
            var target = new Post() { Id = id };

            try
            {
                _context.Remove(target);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Update(PostRequestTO post)
        {
            var p = _mapper.Map<Post>(post);

            if (!Validate(p))
            {
                return false;
            }

            try
            {
                _context.Update(p);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static bool Validate(Post post)
        {
            var contentLen = post.Content.Length;

            if (contentLen < 2 || contentLen > 2048)
            {
                return false;
            }
            return true;
        }
    }
}
