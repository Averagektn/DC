using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Implementation;
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
            AuthorResponseTO? response = null;
            Logger.LogInformation("Creating {res}", Json(author).Value);
            Response.StatusCode = (int)HttpStatusCode.Created;

            try
            {
                response = await AuthorService.Add(author);
            }
            catch (Exception ex)
            {
                Logger.LogError("Invalid request at ADD AUTHOR {ex}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return Json(response);
        }

        [HttpPut]
        public async Task<JsonResult> Update([FromBody] AuthorRequestTO author)
        {
            AuthorResponseTO? response = null;
            Logger.LogInformation("Updating author: {author}", Json(author).Value);

            try
            {
                response = await AuthorService.Update(author);
            }
            catch (Exception ex)
            {
                Logger.LogError("Invalid request at UPDATE AUTHOR {ex}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;  
            }

            return Json(response);
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<JsonResult> PartialUpdate([FromRoute] int id, [FromBody] JsonPatchDocument<Author> author)
        {
            AuthorResponseTO? response = null;
            Logger.LogInformation("Patching {author}", author);

            try
            {
                response = await AuthorService.Patch(id, author);
            }
            catch(Exception ex)
            {
                Logger.LogError("Invalid request at PATCH AUTHOR {ex}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return Json(response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool res = false;
            Logger.LogInformation("Deleted author {id}", id);

            try
            {
                res = await AuthorService.Remove(id);
            }
            catch
            {
                Logger.LogInformation("Deleting failed {id}", id);
            }

            return res ? Ok() : BadRequest();
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
