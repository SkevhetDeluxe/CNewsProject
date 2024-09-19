using CNewsProject.Models.Account;
using CNewsProject.Models.DataBase.Identity;
using CNewsProject.Models.HelperModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CNewsProject.Service
{
    public interface IIdentityService
    {
        public Task<IdentityResultUser> CreateAppUserAsync(User user);
        public Task<string> GenerateEmailTokenAsync(AppUser user);
        public Task<string> GeneratePasswordResetTokenAsync(AppUser user);

		public Task<IdentityResult> ConfirmEmail(AppUser user, string token);
        public Task<AppUser> GetAppUserByIdAsync(string id);
        public Task<AppUser> GetAppUserByEmailAsync(string email);
        public Task<IdentityResult> ResetPassword(AppUser user, string token, string password);

        public Task<AppUser> GetAppUserByClaimsPrincipal(System.Security.Claims.ClaimsPrincipal principal);

        public void FireOnOff(string userId);
        public bool UpdateNewsLetterSetting(AppUser user, NLUserSetting setting);

        public List<string>? GetUserSubscribedToAuthors(AppUser user);




		public IEnumerable<AppUser> ReadAppUsers();
        public Task<IdentityResult> UpdateAppUserAsync(string id, string email, string name, string password);
        public Task<IdentityResult> DeleteUserByIdAsync(string id);


        public Task<Microsoft.AspNetCore.Identity.SignInResult> LoginAppUserAsync(Login login);
        public Task LogoutAppUserAsync();


        public List<int> GetUserLikes(string userId);
        public List<int> GetUserLikes(ClaimsPrincipal principal);

        public void SeedUsers(int amount);

        // ROLES
        #region ROLES
        public IEnumerable<IdentityRole>? ReadRoles();
        public Task<IdentityResult> CreateRoleAsync(string name);
        public Task<IdentityRole>? GetRoleByIdAsync(string id);
        public Task<IdentityResult> DeleteRoleAsync(IdentityRole role);

        public Task SplitUsersByRoleAsync(IdentityRole role, List<AppUser> members, List<AppUser> nonMembers);
        public Task<bool> UserHasRole(AppUser user, string roleName);
        public Task<IList<string>> GetUsersRolesAsync(AppUser user);
        public Task<IdentityResult> GrantUserRoleAsync(AppUser user, string roleName);
        public Task<IdentityResult> PurgeUserRoleAsync(AppUser user, string roleName);




        #endregion

    }
}
