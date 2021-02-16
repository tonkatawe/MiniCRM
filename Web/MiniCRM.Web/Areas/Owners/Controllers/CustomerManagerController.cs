using System.Security.Claims;
using MiniCRM.Web.Infrastructure;
using MiniCRM.Web.ViewModels;

namespace MiniCRM.Web.Areas.Owners.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Web.ViewModels.Customer;
    using MiniCRM.Web.ViewModels.Employees;

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

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber, int? employerId)
        {
            IQueryable<CustomerViewModel> allCustomers;
            var ownerId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //var owner = await this.usersService.GetUserAsync<UserViewModel>(ownerId);

            //if (owner.CompanyId == null)
            //{
            //    return this.RedirectToAction("Create", "Companies", new { area = "Owners" });
            //}
            if (employerId != null)
            {
                allCustomers = this.customersService.GetEmployerCustomers<CustomerViewModel>(ownerId, employerId);
            }
            else
            {
                allCustomers = this.customersService.GetAll<CustomerViewModel>(ownerId);
            }

            this.ViewData["CurrentSort"] = sortOrder;
            this.ViewData["SortByName"] = string.IsNullOrEmpty(sortOrder) ? "nameDesc" : string.Empty;
            this.ViewData["SortByJobTitle"] = string.IsNullOrEmpty(sortOrder) ? "jobDesc" : string.Empty;
            this.ViewData["SortByEmail"] = string.IsNullOrEmpty(sortOrder) ? "emailDesc" : string.Empty;
            this.ViewData["SortByPhone"] = string.IsNullOrEmpty(sortOrder) ? "phoneDesc" : string.Empty;
            this.ViewData["SortByCustomer"] = string.IsNullOrEmpty(sortOrder) ? "customerCount" : string.Empty;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            this.ViewData["CurrentFilter"] = searchString;

            var customers = from c in allCustomers
                            select c;
            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.FullName.Contains(searchString)
                                                 || s.FullName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nameDesc":
                    customers = customers
                        .OrderByDescending(e => e.FullName);
                    break;

                    //case "jobDesc":
                    //    employees = employees.OrderByDescending(e => e.JobTitleName);
                    //    break;
                    //case "emailDesc":
                    //    employees = employees.OrderByDescending(e => e.Email);
                    //    break;
                    //case "phoneDesc":
                    //    employees = employees.OrderByDescending(e => e.PhoneNumber);
                    //    break;
                    //case "customerCount":
                    //    employees = employees.OrderByDescending(e => e.CustomersCount);
                    //    break;
                    //default:
                    //    employees = employees.OrderBy(c => c.LastName);
                    break;
            }

            int pageSize = 3;

            return this.View(await PaginatedList<CustomerViewModel>.CreateAsync(customers, pageNumber ?? 1, pageSize));
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
                return this.RedirectToAction("Create", "Companies", new { area = "Owners" });
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.customersService.CreateAsync(input, owner.Id);

            return this.RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int customerId)
        {
            var owner = await this.userManager.GetUserAsync(this.User);
            var customer = await this.customersService.GetByIdAsync<CustomerViewModel>(customerId);
            if (customer.OwnerId != owner.Id)
            {
                return this.NotFound();
            }

            await this.customersService.DeleteAsync(customerId);

            return this.RedirectToAction("Index");

        }
        public async Task<PartialViewResult> EmployerPartial(int employerId)
        {
            var viewModel = await this.employeesManagerService.GetEmployerAsync<EmployerShortInfoViewModel>(employerId);
            return this.PartialView("_ShortEmployerPartial", viewModel);
        }
    }
}
