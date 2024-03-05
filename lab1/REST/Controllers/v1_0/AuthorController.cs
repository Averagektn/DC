using Microsoft.AspNetCore.Mvc;
using REST.Entity.DTO.RequestTO;
using REST.Service.Interface;

namespace REST.Controllers.V1_0
{
    [Route("api/v1.0/authors")]
    [ApiController]
    public class AuthorController(ILogger<AuthorController> Logger, IAuthorService AuthorService) : Controller
    {
        [HttpGet]
        public async Task<JsonResult> Read()
        {
            var authors = AuthorService.GetAuthors();

            var a = Json(authors);

            return Json(authors);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Create(AuthorRequestTO author)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<IActionResult> Update(AuthorRequestTO author)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(AuthorRequestTO author)
        {
            throw new NotImplementedException();
        }
    }
}
