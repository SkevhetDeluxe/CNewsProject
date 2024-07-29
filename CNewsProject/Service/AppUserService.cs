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
        private readonly IPasswordHasher<AppUser> passwordHasher;


        public AppUserService(ApplicationDbContext db, UserManager<AppUser> userMgr, SignInManager<AppUser> signInMgr,
            RoleManager<IdentityRole> roleMgr, IPasswordHasher<AppUser> passwordHash)
        {
            this.db = db;
            userManager = userMgr;
            signInManager = signInMgr;
            roleManager = roleMgr;
            passwordHasher = passwordHash;
        }


        // CRUD OPERATIONS AppUser ACCOUNTS
        #region CRUD OPERATIONS AppUser Accounts
        // REGISTER New ACCOUNT
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

        public IEnumerable<AppUser> ReadAppUsers()
        {
            return userManager.Users;
        }

        public async Task<AppUser> GetAppUserByIdAsync(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);

            return user;
        }

        public async Task<AppUser> GetAppUserByClaimsPrincipal(System.Security.Claims.ClaimsPrincipal principal)
        {
            AppUser user = await userManager.GetUserAsync(principal);

            return user;
        }

        public async Task<IdentityResult> UpdateAppUserAsync(
            string id, string email, string name, string password)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            
            List<IdentityError> errors = new();
            IdentityResult failed = new();
            bool isGreen = true;

            if (user == null)
            {
                failed.Errors.Append<IdentityError>(new IdentityError()
                { Description = "FyraHundraFyra User not found." });

                return failed;
            }

            if (string.IsNullOrEmpty(email))
            {
                failed.Errors.Append<IdentityError>(new IdentityError()
                { Description = "Email cannot be empty." });
                isGreen = false;
            }

            if (string.IsNullOrEmpty(name))
            {
                failed.Errors.Append<IdentityError>(new IdentityError() 
                { Description = "Password cannot be empty." });
                isGreen = false;
            }

            if (string.IsNullOrEmpty(password))
            {
                failed.Errors.Append<IdentityError>(new IdentityError() 
                { Description = "Password cannot be empty." });
                isGreen = false;
            }

            if (isGreen)
            {
                user.Email = email; 
                user.UserName = name;
                user.PasswordHash = passwordHasher.HashPassword(user, password);
            }
            else
                return IdentityResult.Failed();
            
            IdentityResult result = await userManager.UpdateAsync(user);

            return result;
        }

        public async Task<IdentityResult> DeleteUserByIdAsync(string id)
        {
            AppUser user = await GetAppUserByIdAsync(id);
            IdentityResult result = await userManager.DeleteAsync(user);

            return result;
        }

        

        #endregion

        // LOGIN LOGOUT METHOD
        #region LOGIN LOGOUT METHOD

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

        public async Task LogoutAppUserAsync()
        {
            await signInManager.SignOutAsync();
        }

        #endregion

    }
}
