
using MiniCRM.Services.Data.Contracts;

namespace MiniCRM.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
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
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly INotificationsService notificationsService;

        public HomeController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            INotificationsService notificationsService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.notificationsService = notificationsService;
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

        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await this.userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return this.View("Error");
            }

            var result = await this.userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                this.TempData["ConfirmationEmail"] =
                    "Thank you for confirming your email. You've already sing in. You can list our products";
                await this.signInManager.SignInAsync(user, isPersistent: false);

                await this.notificationsService.CreateNotificationAsync(user.ParentId,
                    $"{user.FullName} has already confirm his email");

                return this.RedirectToAction("ChangePassword", "Home", new { userId = user.Id });
            }
            else
            {
                return this.View("Error");
            }
        }

        [Authorize(Roles = "Customer, Employer, Owner")]
        public IActionResult ChangePassword()
        {
            return this.View();
        }

        [Authorize(Roles = "Customer, Employer, Owner")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel input)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            // TODO: refactor this using applicationmanagment system! :)

            var token = await this.userManager.GeneratePasswordResetTokenAsync(user);
            await this.userManager.ResetPasswordAsync(user, token, input.Password);

            return this.RedirectToAction("Index");
        }

        public async Task<PartialViewResult> TestNote(int employerId)
        {
            var viewModel = employerId;
            return this.PartialView("_ShortEmployerPartial", viewModel);
        }
    }
}
