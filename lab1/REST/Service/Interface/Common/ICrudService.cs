using Microsoft.AspNetCore.JsonPatch;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;

namespace REST.Service.Interface.Common
{
    public interface ICrudService<Entity, RequestTO, ResponseTO> where Entity : class
    {
        Task<bool> Patch(int id, JsonPatchDocument<Entity> patch);
        IList<ResponseTO> GetAll();
        Task<AuthorResponseTO> Add(RequestTO author);
        Task<bool> Remove(int id);
        Task<bool> Update(RequestTO author);
        Task<ResponseTO> GetByID(int id);
    }
}
