using Microsoft.AspNetCore.Mvc;

namespace REST.Controllers.V1_0
{
    [Route("/api/v1.0/markers")]
    public class MarkerController(ILogger<MarkerController> Logger) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
