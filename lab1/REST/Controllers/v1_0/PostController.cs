using AutoMapper;
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
    [Route("api/v1.0/posts")]
    [ApiController]
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
            PostResponseTO? response = null;
            Logger.LogInformation("Creating {res}", Json(post).Value);
            Response.StatusCode = (int)HttpStatusCode.Created;

            try
            {
                response = await PostService.Add(post);
            }
            catch (Exception ex)
            {
                Logger.LogError("Invalid request at ADD POST {ex}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return Json(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PostRequestTO post)
        {
            PostResponseTO? response = null;
            Logger.LogInformation("Updating post: {post}", Json(post).Value);

            try
            {
                response = await PostService.Update(post);
            }
            catch (Exception ex)
            {
                Logger.LogError("Invalid request at UPDATE POST {ex}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return Json(response);
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> PartialUpdate([FromRoute] int id, [FromBody] JsonPatchDocument<Post> post)
        {
            PostResponseTO? response = null;
            Logger.LogInformation("Patching {post}", post);

            try
            {
                response = await PostService.Patch(id, post);
            }
            catch (Exception ex)
            {
                Logger.LogError("Invalid request at PATCH POST {ex}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return Json(response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool res = false;
            Logger.LogInformation("Deleted post {id}", id);

            try
            {
                res = await PostService.Remove(id);
            }
            catch
            {
                Logger.LogInformation("Deleting failed {id}", id);
            }

            return res ? Ok() : BadRequest();
        }
    }
}
