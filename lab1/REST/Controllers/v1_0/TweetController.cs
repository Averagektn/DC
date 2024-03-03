﻿using Microsoft.AspNetCore.Mvc;

namespace REST.Controllers.v1_0
{
    public class TweetController(ILogger logger) : Controller
    {
        private readonly ILogger _logger = logger;

        public IActionResult Index()
        {
            return View();
        }
    }
}
