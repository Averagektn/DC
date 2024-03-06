using Microsoft.AspNetCore.JsonPatch;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;

namespace REST.Service.Interface
{
    public interface IAuthorService
    {
        Task<bool> Patch(int id, JsonPatchDocument<Author> patch);
        IList<AuthorResponseTO> GetAll();
        Task<bool> Add(AuthorRequestTO author);
        Task<bool> Remove(int id);
        Task<bool> Update(AuthorRequestTO author);
    }
}
