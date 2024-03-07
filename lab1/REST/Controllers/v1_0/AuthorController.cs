using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Interface;
using System.Net;

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
        public async Task<JsonResult> Create([FromBody] AuthorRequestTO author)
        {
            Task<AuthorResponseTO>? res = null;
            Logger.LogInformation("Creating {res}", Json(author).Value);

            try
            {
                Response.StatusCode = (int)HttpStatusCode.Created;
                res = AuthorService.Add(author);
            }
            catch (Exception ex)
            {
                Logger.LogError("Invalid request at ADD AUTHOR {ex}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            if (res is not null)
            {
                var a = await res;
                return Json(a);
            }
            return Json(new EmptyResult());
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

            Logger.LogInformation("Deleted author {id}", id);

            return await res ? Ok() : BadRequest();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<JsonResult> GetByID([FromRoute] int id)
        {
            Logger.LogInformation("Get AUTHOR {id}", id);
            AuthorResponseTO? response = null;

            try
            {
                response = await AuthorService.GetByID(id);
            }
            catch
            {
                Logger.LogError("ERROR getting AUTHOR {id}", id);
                Response.StatusCode = (int)HttpStatusCode.NotFound;
            }

            return Json(response);
        }
    }
}
