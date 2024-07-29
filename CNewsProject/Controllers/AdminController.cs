
using CNewsProject.Models.Account;
using CNewsProject.Models.DataBase.Identity;
using CNewsProject.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CNewsProject.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAppUserService appUserService;

        public AdminController(IAppUserService appUserSrvc)
        {
            appUserService = appUserSrvc;
        }


        [AllowAnonymous]
        //[Route("Admin/Users")]
        public IActionResult Users()
        {
            IEnumerable<AppUser> users = appUserService.ReadAppUsers();

            return View(users);
        }
    }
}
