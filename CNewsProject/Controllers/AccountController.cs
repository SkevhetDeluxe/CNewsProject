using CNewsProject.Models.Account;
using CNewsProject.Models.DataBase.Identity;
using CNewsProject.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CNewsProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAppUserService appUserService;

        public AccountController(IAppUserService appUserSrvc)
        {
            appUserService = appUserSrvc;
        }

        // CRUD METHODS
        #region CRUD METHODS

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await appUserService.CreateAppUserAsync(user);

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                {
                    foreach (IdentityError err in result.Errors)
                        ModelState.AddModelError("", err.Description);
                }
            }

            return View(user);
        }

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
                Microsoft.AspNetCore.Identity.SignInResult result = await appUserService.LoginAppUserAsync(login);

                if (result.Succeeded)
                    return Redirect(login.ReturnUrl ?? "/");
                
                ModelState.AddModelError(nameof(login.EmailUsername), "Login Failed: Invalid Email or password");   
            }
            
            return View(login);
        }

        #endregion


        //TESTING PURPOSES
        public IActionResult TestingShit()
        {
            return View();
        }
    }
}
