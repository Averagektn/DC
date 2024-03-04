using Microsoft.AspNetCore.Mvc;

namespace REST.Controllers.V1_0
{
    public class PostController(ILogger logger) : Controller
    {
        private readonly ILogger _logger = logger;

        public IActionResult Index()
        {
            return View();
        }
    }
}
