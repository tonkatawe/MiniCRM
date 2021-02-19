using System;
using Microsoft.AspNetCore.Identity;
using MiniCRM.Data.Models;

namespace MiniCRM.Web.Areas.Employees.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Web.Infrastructure;
    using MiniCRM.Web.ViewModels.Customer;

    public class CustomersController : EmployeesController
    {
        private readonly ICustomersService customersService;
        private readonly IEmployeesManagerService employeesManagerService;
        private readonly INotificationsService notificationsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CustomersController(
            ICustomersService customersService,
            IEmployeesManagerService employeesManagerService,
            INotificationsService notificationsService,
            UserManager<ApplicationUser> userManager)
        {
            this.customersService = customersService;
            this.employeesManagerService = employeesManagerService;
            this.notificationsService = notificationsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var employerAccount = await this.userManager.GetUserAsync(this.User);

            var employerId = await this.employeesManagerService.GetEmployersIdAsync(employerAccount.Id);

            var allCustomers =
                this.customersService.GetEmployerCustomers<CustomerViewModel>(employerAccount.ParentId, employerId);

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
            var employerAccount = await this.userManager.GetUserAsync(this.User);

            var employerId = await this.employeesManagerService.GetEmployersIdAsync(employerAccount.Id);

            var viewModel = new CustomerCreateModel
            {
                EmployerId = employerId,
                OwnerId = employerAccount.ParentId,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateModel input)
        {
            var employerAccount = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.customersService.CreateAsync(input);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.notificationsService.CreateNotificationAsync(
                employerAccount.ParentId,
                $"{employerAccount.FullName} add new customer");

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int customerId)
        {
            var employerAccount = await this.userManager.GetUserAsync(this.User);
            var employerId = await this.employeesManagerService.GetEmployersIdAsync(employerAccount.Id);
            var viewModel = await this.customersService.GetByIdAsync<CustomerEditModel>(customerId);

            if (viewModel.EmployerId != employerId)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(CustomerEditModel input)
        {
            var employerAccount = await this.userManager.GetUserAsync(this.User);
            var employerId = await this.employeesManagerService.GetEmployersIdAsync(employerAccount.Id);

            if (input.EmployerId != employerId)
            {
                return this.NotFound();
            }
            try
            {
                await this.customersService.UpdateAsync(input);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
            }

            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.notificationsService.CreateNotificationAsync(
                employerAccount.ParentId,
                $"{employerAccount.FullName} edited {input.FirstName} {input.LastName} as him customer");

            return this.RedirectToAction("Index");
        }
    }
}
