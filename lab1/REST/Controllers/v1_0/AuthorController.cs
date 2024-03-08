using Microsoft.AspNetCore.Mvc;
using REST.Controllers.V1_0.Common;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Interface;

namespace REST.Controllers.V1_0
{
    [Route("api/v1.0/authors")]
    [ApiController]
    public class AuthorController(IAuthorService AuthorService, ILogger<AuthorController> Logger) :
        AbstractController<Author, AuthorRequestTO, AuthorResponseTO>(AuthorService, Logger)
    { }
}
