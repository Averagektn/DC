using Microsoft.AspNetCore.JsonPatch;

namespace REST.Service.Interface.Common
{
    public interface ICrudService<Entity, RequestTO, ResponseTO> where Entity : class
    {
        Task<ResponseTO> Patch(int id, JsonPatchDocument<Entity> patch);
        IList<ResponseTO> GetAll();
        Task<ResponseTO> Add(RequestTO author);
        Task<bool> Remove(int id);
        Task<ResponseTO> Update(RequestTO author);
        Task<ResponseTO> GetByID(int id);
    }
}
