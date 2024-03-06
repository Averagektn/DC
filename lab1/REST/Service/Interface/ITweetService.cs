using Microsoft.AspNetCore.JsonPatch;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;

namespace REST.Service.Interface
{
    public interface ITweetService
    {
        Task<bool> Patch(int id, JsonPatchDocument<Tweet> patch);
        IList<TweetResponseTO> GetAll();
        Task<bool> Add(TweetRequestTO tweet);
        Task<bool> Remove(int id);
        Task<bool> Update(TweetRequestTO tweet);
    }
}
