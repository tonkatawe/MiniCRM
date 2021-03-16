namespace MiniCRM.Web.Areas.Employees.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
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

        public IActionResult Index()
        {
            return this.View();
            //var employerAccount = await this.userManager.GetUserAsync(this.User);

            //var employerId = await this.employeesManagerService.GetEmployersIdAsync(employerAccount.Id);

            //var allCustomers =
            //    this.customersService.GetEmployerCustomers<CustomerViewModel>(employerAccount.ParentId, employerId);

            //this.ViewData["CurrentSort"] = sortOrder;
            //this.ViewData["SortByName"] = string.IsNullOrEmpty(sortOrder) ? "nameDesc" : string.Empty;
            //this.ViewData["SortByJobTitle"] = string.IsNullOrEmpty(sortOrder) ? "jobDesc" : string.Empty;
            //this.ViewData["SortByEmail"] = string.IsNullOrEmpty(sortOrder) ? "emailDesc" : string.Empty;
            //this.ViewData["SortByPhone"] = string.IsNullOrEmpty(sortOrder) ? "phoneDesc" : string.Empty;
            //this.ViewData["SortByCustomer"] = string.IsNullOrEmpty(sortOrder) ? "customerCount" : string.Empty;

            //if (searchString != null)
            //{
            //    pageNumber = 1;
            //}
            //else
            //{
            //    searchString = currentFilter;
            //}

            //this.ViewData["CurrentFilter"] = searchString;

            //var customers = from c in allCustomers
            //                select c;
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    customers = customers.Where(s => s.FullName.Contains(searchString)
            //                                     || s.FullName.Contains(searchString));
            //}

            //switch (sortOrder)
            //{
            //    case "nameDesc":
            //        customers = customers
            //            .OrderByDescending(e => e.FullName);
            //        break;

            //        //case "jobDesc":
            //        //    employees = employees.OrderByDescending(e => e.JobTitleName);
            //        //    break;
            //        //case "emailDesc":
            //        //    employees = employees.OrderByDescending(e => e.Email);
            //        //    break;
            //        //case "phoneDesc":
            //        //    employees = employees.OrderByDescending(e => e.PhoneNumber);
            //        //    break;
            //        //case "customerCount":
            //        //    employees = employees.OrderByDescending(e => e.CustomersCount);
            //        //    break;
            //        //default:
            //        //    employees = employees.OrderBy(c => c.LastName);
            //        break;
            //}

            //int pageSize = 3;

            //return this.View(await PaginatedList<CustomerViewModel>.CreateAsync(customers, pageNumber ?? 1, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomers()
        {
            var employerAccount = await this.userManager.GetUserAsync(this.User);

            var employerId = await this.employeesManagerService.GetEmployersIdAsync(employerAccount.Id);

            var allCustomers =
                this.customersService.GetEmployerCustomers<CustomerViewModel>(employerAccount.ParentId, employerId);

            var test =
         this.customersService.GetEmployerCustomers<CustomerViewModel>(employerAccount.ParentId, employerId).ToList();

            try
            {
                var draw = this.Request.Form["draw"].FirstOrDefault();
                var start = this.Request.Form["start"].FirstOrDefault();
                var length = this.Request.Form["length"].FirstOrDefault();
                var sortColumn = this.Request.Form["columns[" + this.Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = this.Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = this.Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var customerData = from tempcustomer in allCustomers select tempcustomer;
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.FirstName.Contains(searchValue)
                                                || m.MiddleName.Contains(searchValue)
                                                || m.LastName.Contains(searchValue)
                                                || m.Email.Contains(searchValue));
                }

                recordsTotal = customerData.Count();
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return this.Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }

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
