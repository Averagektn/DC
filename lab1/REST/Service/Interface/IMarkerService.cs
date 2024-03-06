using Microsoft.AspNetCore.JsonPatch;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;

namespace REST.Service.Interface
{
    public interface IMarkerService
    {
        Task<bool> Patch(int id, JsonPatchDocument<Marker> patch);
        IList<MarkerResponseTO> GetAll();
        Task<bool> Add(MarkerRequestTO marker);
        Task<bool> Remove(int id);
        Task<bool> Update(MarkerRequestTO marker);
    }
}
