using Microsoft.AspNetCore.Mvc;

namespace REST.Controllers.V1_0
{
    [Route("/api/v1.0/tweets")]
    public class TweetController(ILogger<TweetController> Logger) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
