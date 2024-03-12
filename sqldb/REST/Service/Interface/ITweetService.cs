using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Interface.Common;

namespace REST.Service.Interface
{
    public interface ITweetService : ICrudService<Tweet, TweetRequestTO, TweetResponseTO>
    {
        Task<TweetResponseTO> GetTweetByParam(IList<string> markerNames, IList<int> markerIds, string authorLogin,
            string title, string content);
    }
}
