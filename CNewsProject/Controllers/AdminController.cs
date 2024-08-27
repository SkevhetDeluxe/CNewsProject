
namespace CNewsProject.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IIdentityService identityService;
        private readonly ISubscriptionService subService;
        private readonly ISubscriptionStatisticsService _statisticsService;

        public AdminController(IIdentityService identitySrvc, ISubscriptionService subServ, ISubscriptionStatisticsService statisticsService)
        {
            identityService = identitySrvc;
            subService = subServ;
			_statisticsService = statisticsService;
		}

		public IActionResult SubscriptionStats()
		{
			var startDate = DateTime.Today.AddDays(-7); // Last week
			var endDate = DateTime.Today;

			var viewModel = new SubscriptionStatisticsViewModel
			{
				TotalSubscribers = _statisticsService.GetTotalSubscribers(),
				NewSubscribersLastWeek = _statisticsService.GetNewSubscribers(startDate, endDate),
				DailySubscriptions = _statisticsService.GetDailySubscriptions(startDate, endDate)
			};

			return View(viewModel);
		}

		public ViewResult Index() => View();

        // USERS
        #region USERS
        //[Route("Admin/Users")]
        public IActionResult Users()
        {
            IEnumerable<AppUser> users = identityService.ReadAppUsers();

            return View(users);
        }

        public async Task<IActionResult> ManageUser(string id)
        {
            AppUser user = await identityService.GetAppUserByIdAsync(id);
            return View(user);
        }

        public async Task<IActionResult> AdminEdit(string id)
        {
            AppUser user = await identityService.GetAppUserByIdAsync(id);
            return View(user);
        }

        [HttpPost]
		public async Task<IActionResult> AdminEdit(string id, string email, string userName, string password)
		{
			AppUser user = await identityService.GetAppUserByIdAsync(id);

            await identityService.UpdateAppUserAsync(id, email, userName, password);

			return RedirectToAction("Index");
		}
        #endregion

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

        // CLAIMS
        #region CLAIMS

        public ViewResult Claims() => View(User?.Claims);

        #endregion

        //SUBSCRIPTIONS
        #region

        public IActionResult RevokeSubTypes()
        {
            return ViewComponent("SubTypes", subService.GetAllTypes());
        }

        public ViewResult SubscriptionTypes() => View(subService.GetAllTypes());

        public IActionResult AddType()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddType(SubscriptionType type)
        {
            if (ModelState.IsValid)
            {
                if (subService.AddType(type))
                {
                    return RedirectToAction("SubscriptionTypes");
                }
            }
            return View(type);
        }

        public IActionResult DeleteType(int id)
        {
            bool result = subService.RemoveType(id);

            if (result)
                return Json(new { succeeded = true });
            else
                return Json(new { succeeded = false });
        }

        public async Task<IActionResult> GiveSub(string userId, int typeId, double days)
        {
            AppUser user = await identityService.GetAppUserByIdAsync(userId);

            bool succeded = subService.AdminGiveSub(user, typeId, days);

            return RedirectToAction("ManageUser", new { id = userId });
        }

        public IActionResult ManageType(int id)
        {
            return View(subService.GetTypeById(id));
        }

        public IActionResult TypeHasUsers(int id)
        {
            if (subService.TypeHasUsers(id))
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
		#endregion
	}
}
