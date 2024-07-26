using CNewsProject.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace CNewsProject.Service
{
    public interface IAppUserService
    {
        public Task<IdentityResult> CreateAppUserAsync(User user);


        public Task<SignInResult> LoginAppUserAsync(Login login);


    }
}
