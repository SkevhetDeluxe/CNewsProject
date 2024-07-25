using Microsoft.AspNetCore.Mvc;

namespace CNewsProject.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Local()
        {
            return View();
        }

        public IActionResult Sweden()
        {
            return View();
        }

        public IActionResult World()
        {
            return View();
        }

        public IActionResult Economy()
        {
            return View();
        }

        public IActionResult Sport()
        {
            return View();
        }
    }
}
