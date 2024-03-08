using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Interface.Common;

namespace REST.Service.Interface
{
    public interface IMarkerService : ICrudService<Marker, MarkerRequestTO, MarkerResponseTO>
    {
        Task<IList<MarkerResponseTO>> GetByTweetID(int tweetId);
    }
}
