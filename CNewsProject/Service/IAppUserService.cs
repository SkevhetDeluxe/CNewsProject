using CNewsProject.Models.Account;
using CNewsProject.Models.DataBase.Identity;
using Microsoft.AspNetCore.Identity;

namespace CNewsProject.Service
{
    public interface IAppUserService
    {
        public Task<IdentityResult> CreateAppUserAsync(User user);
        public Task<AppUser> GetAppUserByIdAsync(string id);
        public Task<AppUser> GetAppUserByClaimsPrincipal(System.Security.Claims.ClaimsPrincipal principal);

		public IEnumerable<AppUser> ReadAppUsers();
        public Task<IdentityResult> UpdateAppUserAsync(string id, string email, string name, string password);
        public Task<IdentityResult> DeleteUserByIdAsync(string id);


        public Task<SignInResult> LoginAppUserAsync(Login login);
        public Task LogoutAppUserAsync();


    }
}
