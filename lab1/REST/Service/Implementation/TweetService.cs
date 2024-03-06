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

        public async Task<bool> Add(TweetRequestTO tweet)
        {
            var t = _mapper.Map<Tweet>(tweet);

            if (!Validate(t))
            {
                return false;
            }

            try
            {
                _context.Tweets.Add(t);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IList<TweetResponseTO> GetAll()
        {
            var res = new List<TweetResponseTO>();

            foreach (var t in _context.Tweets)
            {
                res.Add(_mapper.Map<TweetResponseTO>(t));
            }

            return res;
        }

        public async Task<bool> Patch(int id, JsonPatchDocument<Tweet> patch)
        {
            var target = _context.Find<Tweet>(id);
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
            var target = new Tweet() { Id = id };

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

        public async Task<bool> Update(TweetRequestTO tweet)
        {
            var t = _mapper.Map<Tweet>(tweet);

            if (!Validate(t))
            {
                return false;
            }

            try
            {
                _context.Update(t);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static bool Validate(Tweet tweet)
        {
            var titleLen = tweet.Title.Length;
            var contentLen = tweet.Content.Length;

            if (titleLen < 2 || titleLen > 64)
            {
                return false;
            }
            if (contentLen < 4 || contentLen > 2048)
            {
                return false;
            }
            if (tweet.Modified < tweet.Created)
            {
                return false;
            }
            return true;
        }
    }
}
