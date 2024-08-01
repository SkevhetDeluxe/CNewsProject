using CNewsProject.Models.Account;
using CNewsProject.Models.DataBase.Identity;
using CNewsProject.Models.HelperModels;
using Microsoft.AspNetCore.Identity;

namespace CNewsProject.Service
{
    public interface IIdentityService
    {
        public Task<IdentityResultUser> CreateAppUserAsync(User user);
        public Task<string> GenerateEmailTokenAsync(AppUser user);
        public Task<IdentityResult> ConfirmEmail(AppUser user, string token);
        public Task<AppUser> GetAppUserByIdAsync(string id);
        public Task<AppUser> GetAppUserByEmailAsync(string email);

        public Task<AppUser> GetAppUserByClaimsPrincipal(System.Security.Claims.ClaimsPrincipal principal);

		public IEnumerable<AppUser> ReadAppUsers();
        public Task<IdentityResult> UpdateAppUserAsync(string id, string email, string name, string password);
        public Task<IdentityResult> DeleteUserByIdAsync(string id);


        public Task<SignInResult> LoginAppUserAsync(Login login);
        public Task LogoutAppUserAsync();


        // ROLES
        #region ROLES
        public IEnumerable<IdentityRole>? ReadRoles();
        public Task<IdentityResult> CreateRoleAsync(string name);
        public Task<IdentityRole>? GetRoleByIdAsync(string id);
        public Task<IdentityResult> DeleteRoleAsync(IdentityRole role);

        public Task SplitUsersByRoleAsync(IdentityRole role, List<AppUser> members, List<AppUser> nonMembers);
        public Task<bool> UserHasRole(AppUser user, string roleName);
        public Task<IdentityResult> GrantUserRoleAsync(AppUser user, string roleName);
        public Task<IdentityResult> PurgeUserRoleAsync(AppUser user, string roleName);




        #endregion

    }
}
