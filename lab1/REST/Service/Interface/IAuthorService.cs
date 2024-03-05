using REST.Entity.DTO.ResponseTO;

namespace REST.Service.Interface
{
    public interface IAuthorService
    {
        IList<AuthorResponseTO> GetAuthors();
    }
}
