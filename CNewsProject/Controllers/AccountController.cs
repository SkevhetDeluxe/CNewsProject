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

        // [Route("Account/Register")]
        [AllowAnonymous] // CREATE
        public IActionResult Register()
        {
            return View();
        }

        // [Route("Account/Register")]
        [HttpPost]
        [AllowAnonymous] // CREATE
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


        //Single READ ACCOUnt
		public async Task<IActionResult> MyAccount()
		{
			AppUser user = await appUserService.GetAppUserByClaimsPrincipal(HttpContext.User);

			return View(user);
		}

		// UPDATE ACCOUNT. (We can decide what the Users are allowed to update later.)
		//                 (For now I'm just putting email, user name and password.)

		//[Route("Account/Update")]
		[HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            AppUser user = await appUserService.GetAppUserByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }


        //[Route("Account/Update")] // THIS MADE WHOLE FING ÅPROGHRAM HELT JÄVLA EFTERBLIBET ASSÅ DPON*YT USER PLEASE !THACACKTTACK!!=!=!
        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string userName, string password)
        {
            AppUser user = await appUserService.GetAppUserByIdAsync(id);

            IdentityResult result = await appUserService.UpdateAppUserAsync(id, email, userName, password);

            if (result.Succeeded)
                return RedirectToAction("Index");

            return View(user);
        }

        public async Task<IActionResult> DeleteMyAccount()
        {
            AppUser user = await appUserService.GetAppUserByClaimsPrincipal(HttpContext.User);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMyAccount(string id)
        {
            IdentityResult result = await appUserService.DeleteUserByIdAsync(id);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            return RedirectToAction("DeleteMyAccount");
        }


        #endregion

        // LOGIN LOGOUT METHODS
        #region LOGIN LOGOUT METHODS

        // [Route("Account/Login")]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            Login login = new();
            login.ReturnUrl = returnUrl;
            return View(login);
        }

        // [Route("Account/Login")]
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

                appUserService.LoginAppUserAsync(login);

            }
            
            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await appUserService.LogoutAppUserAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion


        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
