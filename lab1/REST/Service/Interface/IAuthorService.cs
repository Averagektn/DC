using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;

namespace REST.Service.Interface
{
    public interface IAuthorService
    {
        IList<AuthorResponseTO> GetAuthors();
        bool AddAuthor(AuthorRequestTO author);
        bool RemoveAuthor(AuthorRequestTO author);
        bool UpdateAuthor(AuthorRequestTO author);
    }
}
