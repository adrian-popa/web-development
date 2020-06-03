using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebLab9.DataAbstractionLayer;
using WebLab9.Models;

namespace WebLab9.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        private readonly DAL dal;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;

            dal = new DAL();
        }

        [HttpPost]
        public IActionResult Authenticate()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            
            User user = dal.Login(username, password);

            ViewBag.Username = username;

            if (user != null)
            {
                HttpContext.Session.SetString("user_username", user.Username);
                HttpContext.Session.SetString("user_type", user.Type);
                return user.Type == "professor" ? View("~/Views/Home/Professor.cshtml") : View("~/Views/Home/Student.cshtml");
            }
            else
            {
                TempData["Error"] = "Invalid credentials!";

                return View("~/Views/Home/Login.cshtml");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user_username");
            HttpContext.Session.Remove("user_type");

            return View("~/Views/Home/Login.cshtml");
        }
    }
}
