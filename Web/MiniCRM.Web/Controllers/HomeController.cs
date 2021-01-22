using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MiniCRM.Common;
using MiniCRM.Data.Models;

namespace MiniCRM.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
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
                return this.RedirectToAction("Index", "Owners");
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
