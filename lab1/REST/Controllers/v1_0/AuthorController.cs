using Microsoft.AspNetCore.Mvc;
using REST.Controllers.V1_0.Common;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Interface;
using System.Net;

namespace REST.Controllers.V1_0
{
    [Route("api/v1.0/authors")]
    [ApiController]
    public class AuthorController(IAuthorService AuthorService, ILogger<AuthorController> Logger) :
        AbstractController<Author, AuthorRequestTO, AuthorResponseTO>(AuthorService, Logger)
    {
        [HttpGet]
        [Route("tweets/{id:int}")]
        public async Task<JsonResult> GetByTweetID([FromRoute] int id)
        {
            AuthorResponseTO? response = null;
            Logger.LogInformation("Getting author by tweet ID: {id}", id);

            try
            {
                response = await AuthorService.GetByTweetID(id);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error getting author by tweet ID {ex}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return Json(response);
        }
    }
}
