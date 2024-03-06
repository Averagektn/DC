using Microsoft.AspNetCore.JsonPatch;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;

namespace REST.Service.Interface
{
    public interface IPostService
    {
        Task<bool> Patch(int id, JsonPatchDocument<Post> patch);
        IList<PostResponseTO> GetAll();
        Task<bool> Add(PostRequestTO post);
        Task<bool> Remove(int id);
        Task<bool> Update(PostRequestTO post);
    }
}
