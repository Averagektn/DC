using Microsoft.AspNetCore.Mvc;

namespace REST.Controllers.v1_0
{
    public class TweetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
