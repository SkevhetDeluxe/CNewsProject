using CNewsProject.Data;
using CNewsProject.Models.Account;
using CNewsProject.Models.DataBase.Identity;
using Microsoft.AspNetCore.Identity;

namespace CNewsProject.Service
{
    public class AppUserService : IAppUserService
    {
        private ApplicationDbContext db;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public AppUserService(ApplicationDbContext db, UserManager<AppUser> userMgr, SignInManager<AppUser> signInMgr,
            RoleManager<IdentityRole> roleMgr)
        {
            this.db = db;
            userManager = userMgr;
            signInManager = signInMgr;
            roleManager = roleMgr;
        }


        // CRUD OPERATIONS AppUser ACCOUNTS
        #region CRUD OPERATIONS AppUser Accounts
        public async Task<IdentityResult> CreateAppUserAsync(User user)
        {
            AppUser appUser = new AppUser
            {
                UserName = user.UserName,
                Email = user.Email
            };

            IdentityResult result = await userManager.CreateAsync(appUser, user.Password);

            return result;
        }
        #endregion

        // LOGIN LOGOUT METHODS
        #region LOGIN LOGOUT METHODS

        public async Task<SignInResult> LoginAppUserAsync(Login login)
        {
            AppUser appUser = await userManager.FindByEmailAsync(login.EmailUsername);

            if (appUser == null)
                appUser = await userManager.FindByNameAsync(login.EmailUsername);

            if (appUser != null)
            {
                await signInManager.SignOutAsync();

                SignInResult result = await signInManager.PasswordSignInAsync(appUser, login.Password, login.RememberMe, false);

                return result;
            }

            return SignInResult.Failed;
        }

        #endregion

    }
}
