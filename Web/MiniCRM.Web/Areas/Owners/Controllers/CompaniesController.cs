using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MiniCRM.Data.Models;
using MiniCRM.Web.ViewModels.Companies;

namespace MiniCRM.Web.Areas.Owners.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Web.Areas.Owner.Controllers;

    public class CompaniesController : OwnersController
    {
        private readonly UserManager<ApplicationUser> userManager;

        public CompaniesController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create(string userId)
        {

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyCreateModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return this.RedirectToAction("Index", "Dashboard");

        }
    }
}
