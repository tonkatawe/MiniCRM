using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MiniCRM.Data.Models;
using MiniCRM.Services.Data.Contracts;
using MiniCRM.Web.ViewModels.Customer;
using MiniCRM.Web.ViewModels.Employees;

namespace MiniCRM.Web.Areas.Owners.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class CustomerManagerController : OwnersController
    {
        private readonly IEmployeesManagerService employeesManagerService;
        private readonly ICustomersService customersService;
        private readonly UserManager<ApplicationUser> userManager;

        public CustomerManagerController(
            IEmployeesManagerService employeesManagerService,
            ICustomersService customersService,
            UserManager<ApplicationUser> userManager)
        {
            this.employeesManagerService = employeesManagerService;
            this.customersService = customersService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Create()
        {
            var owner = await this.userManager.GetUserAsync(this.User);
            var employees = this.employeesManagerService.GetAll<EmployeesDropDownViewModel>(owner.CompanyId).ToList();
            var viewModel = new CustomerCreateModel
            {
                OwnerId = owner.Id,
                Employees = employees,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateModel input)
        {
            var owner = await this.userManager.GetUserAsync(this.User);
            if (owner.CompanyId == null)
            {
                return this.RedirectToAction("Create", "Companies", new {area = "Owners"});
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.customersService.CreateAsync(input, owner.Id);

            return this.RedirectToAction("Index");
        }

        public async Task<PartialViewResult> EmployerPartial(int employerId)
        {
            var viewModel = await this.employeesManagerService.GetEmployerAsync<EmployerShortInfoViewModel>(employerId);
            return this.PartialView("_ShortEmployerPartial", viewModel);
        }
    }
}
