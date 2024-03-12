using Newtonsoft.Json;

namespace REST.Entity.DTO.ResponseTO
{
    public record class AuthorResponseTO(
        int Id, 
        string Login, 
        [property:JsonProperty("firstname")]string FirstName, 
        [property:JsonProperty("lastname")]string LastName);
}
