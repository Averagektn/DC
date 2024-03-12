using Newtonsoft.Json;

namespace REST.Entity.DTO.ResponseTO
{
    public record class AuthorResponseTO(
        [property:JsonProperty("id")] int Id, 
        [property:JsonProperty("login")] string Login, 
        [property:JsonProperty("firstname")]string FirstName, 
        [property:JsonProperty("lastname")]string LastName);
}
