
namespace CNewsProject.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController(
        IIdentityService identitySrvc,
        ISubscriptionService subServ,
        ISubscriptionStatisticsService statisticsService,
        IArticleService articleService)
        : Controller
    {
        public IActionResult SubscriptionStats()
		{
			var startDate = DateTime.Today.AddDays(-7); // Last week
			var endDate = DateTime.Today;

			var viewModel = new SubscriptionStatisticsViewModel
			{
				TotalSubscribers = statisticsService.GetTotalSubscribers(),
				NewSubscribersLastWeek = statisticsService.GetNewSubscribers(startDate, endDate),
				DailySubscriptions = statisticsService.GetDailySubscriptions(startDate, endDate)
			};

			return View(viewModel);
		}

		public ViewResult Index() => View();

        // USERS
        #region USERS
        //[Route("Admin/Users")]
        public IActionResult Users()
        {
            IEnumerable<AppUser> users = identitySrvc.ReadAppUsers();

            return View(users);
        }

        public async Task<IActionResult> ManageUser(string id)
        {
            AppUser user = await identitySrvc.GetAppUserByIdAsync(id);
            return View(user);
        }

        public async Task<IActionResult> AdminEdit(string id)
        {
            AppUser user = await identitySrvc.GetAppUserByIdAsync(id);
            return View(user);
        }

        [HttpPost]
		public async Task<IActionResult> AdminEdit(string id, string email, string userName, string password)
		{
			AppUser user = await identitySrvc.GetAppUserByIdAsync(id);

            await identitySrvc.UpdateAppUserAsync(id, email, userName, password);

			return RedirectToAction("Index");
		}
        #endregion

        // ROLES
        #region ROLES

        public IActionResult Roles()
        {
            IEnumerable<IdentityRole> roles = identitySrvc.ReadRoles();

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
                IdentityResult result = await identitySrvc.CreateRoleAsync(name);
                if (result.Succeeded)
                    return RedirectToAction("Roles");
                
            }
            return View(name);
        }

        public async Task<IActionResult> UpdateRole(string id)
        {
            IdentityRole role = await identitySrvc.GetRoleByIdAsync(id);

            List<AppUser> members = new();
            List<AppUser> nonMembers = new();

            if (role != null)
            {
                await identitySrvc.SplitUsersByRoleAsync(role, members, nonMembers);

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
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    AppUser user = await identitySrvc.GetAppUserByIdAsync(userId);
                    
                    if (user != null)
                    {
                        await identitySrvc.GrantUserRoleAsync(user, model.RoleName);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    AppUser user = await identitySrvc.GetAppUserByIdAsync(userId);

                    if (user != null)
                    {
                        await identitySrvc.PurgeUserRoleAsync(user, model.RoleName);
                    }
                }

                return RedirectToAction("Roles");
            }

            return await UpdateRole(model.RoleId);
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await identitySrvc.GetRoleByIdAsync(id);

            if (role != null)
            {
                IdentityResult result = await identitySrvc.DeleteRoleAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Roles");
            }

            ModelState.AddModelError("", "No ROLE Found brorsan!");
            return RedirectToAction("Roles");
        }

        #endregion

        // CLAIMS
        #region CLAIMS

        public ViewResult Claims() => View(User?.Claims);

        #endregion

        //SUBSCRIPTIONS
        #region

        public IActionResult RevokeSubTypes()
        {
            return ViewComponent("SubTypes", subServ.GetAllTypes());
        }

        public ViewResult SubscriptionTypes() => View(subServ.GetAllTypes());

        public IActionResult AddType()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddType(SubscriptionType type)
        {
            if (ModelState.IsValid)
            {
                if (subServ.AddType(type))
                {
                    return RedirectToAction("SubscriptionTypes");
                }
            }
            return View(type);
        }

        public IActionResult DeleteType(int id)
        {
            bool result = subServ.RemoveType(id);

            if (result)
                return Json(new { succeeded = true });
            else
                return Json(new { succeeded = false });
        }

        public async Task<IActionResult> GiveSub(string userId, int typeId, double days)
        {
            AppUser user = await identitySrvc.GetAppUserByIdAsync(userId);

            bool succeded = subServ.AdminGiveSub(user, typeId, days);

            return RedirectToAction("ManageUser", new { id = userId });
        }

        public IActionResult ManageType(int id)
        {
            return View(subServ.GetTypeById(id));
        }

        public IActionResult TypeHasUsers(int id)
        {
            if (subServ.TypeHasUsers(id))
                return Json(new { hasUsers = true });
            else
                return Json(new { hasUsers = false });
        }

        #endregion

        #region SHhhhhhhhhhhhhhhhhhhhhHhHhhhHhHHHhh
        public IActionResult SugMinaStats()
        {
            return RedirectToAction("SugMinaStats", "News");
        }

        public IActionResult SeedUsers(int amount)
        {
            identitySrvc.SeedUsers(amount);
            return RedirectToAction("Area51");
        }
        #endregion

        // STATISTICS
        #region STATISTICS

        public IActionResult Statistics()
        {
            var users = subServ.GetAllSubscription();
            CNewsProject.Models.Statistics.Statistics stats = new();

            var newCArr = stats.CountNewCustomer(users);

            return View("ShowStatistics", newCArr);
        }

        #endregion


        public IActionResult Area51()
        {
            var model = StaticTempData.AuthorNames.UserNames;

            return View(model);
        }
	}
}
