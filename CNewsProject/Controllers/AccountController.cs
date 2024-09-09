using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Controllers
{
    [Authorize]
    public class AccountController(
        IIdentityService identitySrvc,
        IConfiguration config,
        ISubscriptionService subscriptionService) : Controller
    {
        private readonly EmailHelper _emailSender = new(config);

        public IActionResult Index()
        {
            return RedirectToAction("Profile");
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
                IdentityResultUser resultUser = await identitySrvc.CreateAppUserAsync(user);

                if (resultUser.Result.Succeeded)
                {
                    string token = identitySrvc.GenerateEmailTokenAsync(resultUser.User).Result;
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { token, email = resultUser.User.Email }, Request.Scheme);

                    bool emailSent = _emailSender.SendEmailAsync(resultUser.User.Email, "Confirm Your Email.",
                        confirmationLink!);

                    if (!emailSent)
                        return RedirectToAction();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (IdentityError err in resultUser.Result.Errors)
                        ModelState.AddModelError("", err.Description);
                }
            }

            return View(user);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            AppUser user = await identitySrvc.GetAppUserByEmailAsync(email);

            IdentityResult result = await identitySrvc.ConfirmEmail(user, token);

            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //Single READ ACCOUnt
        public IActionResult Profile()
        {
            //AppUser user = await identitySrvc.GetAppUserByClaimsPrincipal(User);
            //
            // Subscription user = subscriptionService.GetSubscriptionByAppUser(User);

            UserProfileVM vModel = new()
            {
                User = identitySrvc.GetAppUserByClaimsPrincipal(User).Result,
                SubInfo = subscriptionService.GetSubscriptionByAppUser(User)
            };
            
            
            return View(vModel);
        }

        //[Route("/Account/MyAccount")]
        [HttpPost]
        public IActionResult Profile(UserProfileVM vModel)
        {
            //          if (identitySrvc.GetAppUserByIdAsync(user.Id).Result.Fire != user.Fire)
            // 	identitySrvc.FireOnOff(user.Id);
            //
            //user = identitySrvc.GetAppUserByIdAsync(user.Id).Result;

            return View(vModel);
        }

        //Profile RELATED stuff

        #region Profile RElated shit

        #endregion

        // UPDATE ACCOUNT. (We can decide what the Users are allowed to update later.)
        //                 (For now I'm just putting email, user name and password.)

        //[Route("Account/Update")]
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            AppUser user = await identitySrvc.GetAppUserByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }


        //[Route("Account/Update")] // THIS MADE WHOLE FING ÅPROGHRAM HELT JÄVLA EFTERBLIBET ASSÅ DPON*YT USER PLEASE !THACACKTTACK!!=!=!
        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string userName, string password)
        {
            AppUser user = await identitySrvc.GetAppUserByIdAsync(id);

            IdentityResult result = await identitySrvc.UpdateAppUserAsync(id, email, userName, password);

            if (result.Succeeded)
                return RedirectToAction("Index");

            return View(user);
        }

        public async Task<IActionResult> DeleteMyAccount()
        {
            AppUser user = await identitySrvc.GetAppUserByClaimsPrincipal(HttpContext.User);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMyAccount(string id)
        {
            IdentityResult result = await identitySrvc.DeleteUserByIdAsync(id);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            return RedirectToAction("DeleteMyAccount");
        }

        #endregion

        // LOGIN LOGOUT METHODS

        #region LOGIN LOGOUT METHODS

        // [Route("Account/Login")]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl)
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
                Microsoft.AspNetCore.Identity.SignInResult result = await identitySrvc.LoginAppUserAsync(login);

                if (result.Succeeded)
                    return Redirect(login.ReturnUrl ?? "/");

                ModelState.AddModelError(nameof(login.EmailUsername), "Login Failed: Invalid Email or password");

                await identitySrvc.LoginAppUserAsync(login);
            }

            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await identitySrvc.LogoutAppUserAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            if (!ModelState.IsValid)
                return View(email);

            AppUser user = await identitySrvc.GetAppUserByEmailAsync(email);
            if (user == null)
                return RedirectToAction("ForgotPassword");

            var token = await identitySrvc.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme).Trim();

            EmailHelper emailHelper = new(config);
            bool sentEmail = emailHelper.SendEmailAsync(user.Email!, "Reset Password", link!);

            if (sentEmail)
                return View("ForgotPasswordConfirmation");

            return View(email);
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            PasswordReset model = new() { Token = token, Email = email };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(PasswordReset resetPassword)
        {
            if (!ModelState.IsValid)
                return View(resetPassword);

            var user = await identitySrvc.GetAppUserByEmailAsync(resetPassword.Email);
            if (user == null)
                RedirectToAction("ResetPasswordConfirmation");

            IdentityResult result = await identitySrvc.ResetPassword(user, resetPassword.Token, resetPassword.Password);

            if (!result.Succeeded)
                return View();

            return RedirectToAction("ResetPasswordConfirmation");
        }

        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #endregion


        [Route("/Door/Bouncer")]
        public IActionResult Denied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}