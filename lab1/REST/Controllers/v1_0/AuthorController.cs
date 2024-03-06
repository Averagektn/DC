using Microsoft.AspNetCore.Mvc;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Interface;

namespace REST.Controllers.V1_0
{
    [Route("api/v1.0/authors")]
    [ApiController]
    public class AuthorController(ILogger<AuthorController> Logger, IAuthorService AuthorService) : Controller
    {
        [HttpGet]
        public JsonResult Read()
        {
            var authors = AuthorService.GetAuthors();

            Logger.LogInformation("Authors read");

            return Json(authors);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AuthorRequestTO author)
        {
            var res = AuthorService.AddAuthor(author);

            Logger.LogInformation("Creating {res}", Json(author).Value);

            return res ? Ok() : BadRequest();
        }

        [HttpPut]
        public IActionResult Update([FromBody] AuthorRequestTO author)
        {
            var res = AuthorService.UpdateAuthor(author);

            Logger.LogInformation("Updated author: {author}", Json(author).Value);

            return res ? Ok() : BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] int id)
        {
            var res = AuthorService.RemoveAuthor(id);

            Logger.LogInformation("Deleted {author}", Json(author).Value);

            return res ? Ok() : BadRequest();
        }
    }
}
