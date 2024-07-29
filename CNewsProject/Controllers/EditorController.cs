using Microsoft.AspNetCore.Mvc;

namespace CNewsProject.Controllers
{
    public class EditorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
