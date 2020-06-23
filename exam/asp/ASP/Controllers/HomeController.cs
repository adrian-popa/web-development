using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASP.Models;

namespace ASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string username = HttpContext.Session.GetString("user_username");
            string type = HttpContext.Session.GetString("user_type");

            ViewBag.Username = username;

            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            if (username != null)
            {
                return type == "professor" ? View("~/Views/Home/Professor.cshtml") : View("~/Views/Home/Student.cshtml");
            }

            return View("~/Views/Home/Login.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
