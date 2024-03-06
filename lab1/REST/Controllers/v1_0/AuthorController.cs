using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
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
            var authors = AuthorService.GetAll();

            Logger.LogInformation("Authors read");

            return Json(authors);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorRequestTO author)
        {
            var res = AuthorService.Add(author);

            Logger.LogInformation("Creating {res}", Json(author).Value);

            return await res ? Created() : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AuthorRequestTO author)
        {
            var res = AuthorService.Update(author);

            Logger.LogInformation("Updated author: {author}", Json(author).Value);

            return await res ? Ok() : BadRequest();
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> PartialUpdate([FromRoute] int id, [FromBody] JsonPatchDocument<Author> author)
        {
            var res = AuthorService.Patch(id, author);

            Logger.LogInformation("Patched {author}", author);

            return await res ? Ok() : BadRequest();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var res = AuthorService.Remove(id);

            Logger.LogInformation("Deleted {id}", id);

            return await res ? Ok() : BadRequest();
        }
    }
}
