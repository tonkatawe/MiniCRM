namespace MiniCRM.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Common;
    using MiniCRM.Data.Models;
    using MiniCRM.Web.ViewModels;

    public class HomeController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            var user = await this.userManager.GetUserAsync(this.User);

            if (user == null)
            {
                return this.View();
            }

            if (this.User.IsInRole(GlobalConstants.OwnerUserRoleName))
            {
                if (user.CompanyId == null)
                {
                    return this.RedirectToAction("Create", "Companies", new { area = "Owners", userId = user.Id });
                }

                return this.RedirectToAction("Index", "Dashboard", new { area = "Owners" });
            }

            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.RedirectToAction("Index", "Dashboard", new { area = "Administration" });
            }

            return this.View();
        }

        public IActionResult Test()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
