using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Service.Implementation;
using REST.Service.Interface;

namespace REST.Controllers.V1_0
{
    [Route("/api/v1.0/posts")]
    public class PostController(ILogger<PostController> Logger, IPostService PostService) : Controller
    {
        [HttpGet]
        public JsonResult Read()
        {
            var authors = PostService.GetAll();

            Logger.LogInformation("Posts read");

            return Json(authors);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostRequestTO post)
        {
            var res = PostService.Add(post);

            Logger.LogInformation("Creating {res}", Json(post).Value);

            return await res ? Created() : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PostRequestTO post)
        {
            var res = PostService.Update(post);

            Logger.LogInformation("Updated author: {post}", Json(post).Value);

            return await res ? Ok() : BadRequest();
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> PartialUpdate([FromRoute] int id, [FromBody] JsonPatchDocument<Post> post)
        {
            var res = PostService.Patch(id, post);

            Logger.LogInformation("Patched {post}", post);

            return await res ? Ok() : BadRequest();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var res = PostService.Remove(id);

            Logger.LogInformation("Deleted post {id}", id);

            return await res ? Ok() : BadRequest();
        }
    }
}
