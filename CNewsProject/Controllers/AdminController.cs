using Microsoft.AspNetCore.Mvc;

namespace CNewsProject.Controllers
{
    public class AdminController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EditeArtical()
        {
           return RedirectToAction("Index");
        }
        public IActionResult EditeCategory()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Removemember()
        {
            return RedirectToAction("Index", "Home");   
        }
        public IActionResult Details()
        {
            return View();
        }
        //public AdminController<IActionResult> DeleteCategory(int id)
        //{
            
        //    return RedirectToAction((DeleteCategory));
        //}

        //private AdminController<IActionResult> RedirectToAction(Func<int, AdminController<IActionResult>> deleteCategory)
        //{
        //    throw new NotImplementedException();
        //}
    }

   
}
