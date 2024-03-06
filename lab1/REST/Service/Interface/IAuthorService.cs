using Microsoft.AspNetCore.JsonPatch;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;

namespace REST.Service.Interface
{
    public interface IAuthorService
    {
        Task<bool> Patch(int id, JsonPatchDocument<Author> patch);
        IList<AuthorResponseTO> GetAuthors();
        Task<bool> AddAuthor(AuthorRequestTO author);
        Task<bool> RemoveAuthor(int id);
        Task<bool> UpdateAuthor(AuthorRequestTO author);
    }
}
