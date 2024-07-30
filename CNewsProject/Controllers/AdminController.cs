
using CNewsProject.Models.Account;
using CNewsProject.Models.DataBase.Identity;
using CNewsProject.Models.Roles;
using CNewsProject.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CNewsProject.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IIdentityService identityService;

        public AdminController(IIdentityService identitySrvc)
        {
            identityService = identitySrvc;
        }

        public ViewResult Index() => View();


        //[Route("Admin/Users")]
        public IActionResult Users()
        {
            IEnumerable<AppUser> users = identityService.ReadAppUsers();

            return View(users);
        }


        // ROLES
        #region ROLES

        public IActionResult Roles()
        {
            IEnumerable<IdentityRole> roles = identityService.ReadRoles();

            return View(roles);
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await identityService.CreateRoleAsync(name);
                if (result.Succeeded)
                    return RedirectToAction("Roles");
                
            }
            return View(name);
        }

        public async Task<IActionResult> UpdateRole(string id)
        {
            IdentityRole role = await identityService.GetRoleByIdAsync(id);

            List<AppUser> members = new();
            List<AppUser> nonMembers = new();

            if (role != null)
            {
                await identityService.SplitUsersByRoleAsync(role, members, nonMembers);

                return View(new RoleEdit
                {
                    Role = role,
                    Members = members,
                    NonMembers = nonMembers
                });
            }

            return RedirectToAction("Roles");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    AppUser user = await identityService.GetAppUserByIdAsync(userId);
                    
                    if (user != null)
                    {
                        result = await identityService.GrantUserRoleAsync(user, model.RoleName);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    AppUser user = await identityService.GetAppUserByIdAsync(userId);

                    if (user != null)
                    {
                        result = await identityService.PurgeUserRoleAsync(user, model.RoleName);
                    }
                }

                return RedirectToAction("Roles");
            }

            return await UpdateRole(model.RoleId);
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await identityService.GetRoleByIdAsync(id);

            if (role != null)
            {
                IdentityResult result = await identityService.DeleteRoleAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Roles");
            }

            ModelState.AddModelError("", "No ROLE Found brorsan!");
            return RedirectToAction("Roles");
        }

        #endregion
    }
}
