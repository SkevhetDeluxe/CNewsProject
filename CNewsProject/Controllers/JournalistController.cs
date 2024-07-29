using Microsoft.AspNetCore.Mvc;

namespace CNewsProject.Controllers
{
    public class JournalistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
