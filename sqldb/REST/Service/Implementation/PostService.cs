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

        public async Task<PostResponseTO> Add(PostRequestTO post)
        {
            var p = _mapper.Map<Post>(post);

            if (!Validate(p))
            {
                throw new InvalidDataException("POST is not valid");
            }

            _context.Posts.Add(p);
            await _context.SaveChangesAsync();

            return _mapper.Map<PostResponseTO>(p);
        }

        public IList<PostResponseTO> GetAll()
        {
            return _context.Posts.Select(_mapper.Map<PostResponseTO>).ToList();
        }

        public async Task<PostResponseTO> Patch(int id, JsonPatchDocument<Post> patch)
        {
            var target = await _context.FindAsync<Post>(id)
                ?? throw new ArgumentNullException($"POST {id} not found at PATCH {patch}");

            patch.ApplyTo(target);
            await _context.SaveChangesAsync();

            return _mapper.Map<PostResponseTO>(target);
        }

        public async Task<bool> Remove(int id)
        {
            var target = new Post() { Id = id };

            _context.Remove(target);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<PostResponseTO> Update(PostRequestTO post)
        {
            var p = _mapper.Map<Post>(post);

            if (!Validate(p))
            {
                throw new InvalidDataException($"UPDATE invalid data: {post}");
            }

            _context.Update(p);
            await _context.SaveChangesAsync();

            return _mapper.Map<PostResponseTO>(p);
        }

        public async Task<PostResponseTO> GetByID(int id)
        {
            var a = await _context.Posts.FindAsync(id);

            return a is not null ? _mapper.Map<PostResponseTO>(a)
                : throw new ArgumentNullException($"Not found POST {id}");
        }

        public Task<IList<Post>> GetByTweetID(int tweetId)
        {
            throw new NotImplementedException();
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
