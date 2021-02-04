namespace MiniCRM.Web.Areas.Owners.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Web.ViewModels.Companies;

    public class CompaniesController : OwnersController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICompaniesService companiesService;

        public CompaniesController(
            UserManager<ApplicationUser> userManager,
            ICompaniesService companiesService)
        {
            this.userManager = userManager;
            this.companiesService = companiesService;
        }

        public async Task<IActionResult> Create(string userId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.CompanyId != null)
            {
                return this.RedirectToAction("Index", "Dashboard");

            }

            var viewModel = new CompanyCreateModel
            {
                UserId = userId,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyCreateModel input)
        {

            var user = await this.userManager.GetUserAsync(this.User);

            if (user.CompanyId != null)
            {
                return this.RedirectToAction("Index", "Dashboard");

            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.companiesService.CreateAsync(input);

            return this.RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Edit()
        {
            return this.View();
        }
    }
}
