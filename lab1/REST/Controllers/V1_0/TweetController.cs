using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Service.Implementation;
using REST.Service.Interface;

namespace REST.Controllers.V1_0
{
    [Route("/api/v1.0/tweets")]
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
            var res = TweetService.Add(tweet);

            Logger.LogInformation("Creating {res}", Json(tweet).Value);

            return await res ? Created() : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TweetRequestTO tweet)
        {
            var res = TweetService.Update(tweet);

            Logger.LogInformation("Updated tweet: {tweet}", Json(tweet).Value);

            return await res ? Ok() : BadRequest();
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> PartialUpdate([FromRoute] int id, [FromBody] JsonPatchDocument<Tweet> tweet)
        {
            var res = TweetService.Patch(id, tweet);

            Logger.LogInformation("Patched {post}", tweet);

            return await res ? Ok() : BadRequest();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var res = TweetService.Remove(id);

            Logger.LogInformation("Deleted tweet {id}", id);

            return await res ? Ok() : BadRequest();
        }
    }
}
