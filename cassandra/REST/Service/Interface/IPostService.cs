using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Interface.Common;

namespace REST.Service.Interface
{
    public interface IPostService : ICrudService<Post, PostRequestTO, PostResponseTO>
    {
        Task<IList<Post>> GetByTweetID(int tweetId);
    }
}
