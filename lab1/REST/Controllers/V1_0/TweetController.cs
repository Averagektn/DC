using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Implementation;
using REST.Service.Interface;
using System.Net;

namespace REST.Controllers.V1_0
{
    [Route("api/v1.0/tweets")]
    [ApiController]
    public class TweetController(ILogger<TweetController> Logger, ITweetService TweetService) : Controller
    {
        [HttpGet]
        public JsonResult Read()
        {
            var authors = TweetService.GetAll();

            Logger.LogInformation("Tweets read");

            return Json(authors);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TweetRequestTO tweet)
        {
            TweetResponseTO? response = null;
            Logger.LogInformation("Creating {res}", Json(tweet).Value);
            Response.StatusCode = (int)HttpStatusCode.Created;

            try
            {
                response = await TweetService.Add(tweet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Invalid request at ADD TWEET {ex}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return Json(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TweetRequestTO tweet)
        {
            TweetResponseTO? response = null;
            Logger.LogInformation("Updating post: {tweet}", Json(tweet).Value);

            try
            {
                response = await TweetService.Update(tweet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Invalid request at UPDATE TWEET {ex}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return Json(response);
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> PartialUpdate([FromRoute] int id, [FromBody] JsonPatchDocument<Tweet> tweet)
        {
            TweetResponseTO? response = null;
            Logger.LogInformation("Patching {tweet}", tweet);

            try
            {
                response = await TweetService.Patch(id, tweet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Invalid request at PATCH TWEET {ex}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return Json(response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool res = false;
            Logger.LogInformation("Deleted tweet {id}", id);

            try
            {
                res = await TweetService.Remove(id);
            }
            catch
            {
                Logger.LogInformation("Deleting failed {id}", id);
            }

            return res ? Ok() : BadRequest();
        }
    }
}
