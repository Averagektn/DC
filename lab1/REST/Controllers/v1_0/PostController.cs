using Microsoft.AspNetCore.Mvc;

namespace REST.Controllers.V1_0
{
    [Route("/api/v1.0/posts")]
    public class PostController(ILogger<PostController> Logger) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
