using CNewsProject.Models.Account;
using CNewsProject.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CNewsProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private AppUserService appUserService;

        public AccountController(AppUserService appUserSrvc)
        {
            appUserService = appUserSrvc;
        }

        // CRUD METHODS
        #region CRUD METHODS

        #endregion

        // LOGIN LOGOUT METHODS
        #region LOGIN LOGOUT METHODS

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            Login login = new();
            login.ReturnUrl = returnUrl;
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                appUserService.LoginAppUserAsync(login);
            }
        }

        #endregion
    }
}
