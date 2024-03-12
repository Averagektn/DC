using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Interface;
using REST.Storage.Common;

namespace REST.Service.Implementation
{
    public class TweetService(DbStorage dbStorage, IMapper mapper) : ITweetService
    {
        private readonly DbStorage _context = dbStorage;
        private readonly IMapper _mapper = mapper;

        public async Task<TweetResponseTO> Add(TweetRequestTO tweet)
        {
            var t = _mapper.Map<Tweet>(tweet);
            var author = await _context.Authors.FindAsync(t.AuthorId) ?? throw new ArgumentNullException($"AUTHOR not found {t.Author.Id}");

            if (!Validate(t))
            {
                throw new InvalidDataException("TWEET is not valid");
            }

            t.Author = author;
            _context.Tweets.Add(t);
            await _context.SaveChangesAsync();

            return _mapper.Map<TweetResponseTO>(t);
        }

        public IList<TweetResponseTO> GetAll()
        {
            return _context.Tweets.Select(_mapper.Map<TweetResponseTO>).ToList();
        }

        public async Task<TweetResponseTO> Patch(int id, JsonPatchDocument<Tweet> patch)
        {
            var target = await _context.Tweets.FirstAsync(t => t.Id == id)
                ?? throw new ArgumentNullException($"TWEET {id} not found at PATCH {patch}");

            patch.ApplyTo(target);
            await _context.SaveChangesAsync();

            return _mapper.Map<TweetResponseTO>(target);
        }

        public async Task<bool> Remove(int id)
        {
            var target = new Tweet() { Id = id };

            _context.Remove(target);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<TweetResponseTO> Update(TweetRequestTO tweet)
        {
            var t = _mapper.Map<Tweet>(tweet);

            if (!Validate(t))
            {
                throw new InvalidDataException($"UPDATE invalid data: {tweet}");
            }

            _context.Update(t);
            await _context.SaveChangesAsync();

            return _mapper.Map<TweetResponseTO>(t);
        }

        public Task<TweetResponseTO> GetTweetByParam(IList<string> markerNames, IList<int> markerIds, string authorLogin, string title, string content)
        {
            throw new NotImplementedException();
        }

        public async Task<TweetResponseTO> GetByID(int id)
        {

            var a = await _context.Tweets.FirstAsync(t => t.Id == id);

            return a is not null ? _mapper.Map<TweetResponseTO>(a)
                : throw new ArgumentNullException($"Not found TWEET {id}");
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
