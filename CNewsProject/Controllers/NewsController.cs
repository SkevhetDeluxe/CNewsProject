using Microsoft.AspNetCore.Mvc;

namespace CNewsProject.Controllers
{
    [Route("news")]
    public class NewsController : Controller
    {        
        [Route("local")]
        public IActionResult Local()
        {
            return View();
        }

        [Route("sweden")]

        public IActionResult Sweden()
        {
            return View();
        }

        [Route("world")]

        public IActionResult World()
        {
            return View();
        }

        [Route("economy")]

        public IActionResult Economy()
        {
            return View();
        }

        [Route("sport")]

        public IActionResult Sport()
        {
            return View();
        }
    }
}
