namespace MiniCRM.Web.Areas.Owners.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Security.Claims;
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


        public IActionResult Index(int? employerId)
        {
            this.ViewData["EmployerId"] = employerId;
            return this.View();
        }

        [HttpPost]
        public IActionResult GetCustomers(int? employerId)
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

        public async Task<IActionResult> Edit(int customerId)
        {
            var owner = await this.userManager.GetUserAsync(this.User);
            var viewModel = await this.customersService.GetByIdAsync<CustomerEditModel>(customerId);

            if (owner.Id != viewModel.OwnerId)
            {
                return NotFound();
            }

            var employees = this.employeesManagerService.GetAll<EmployeesDropDownViewModel>(owner.CompanyId).ToList();
            viewModel.Employees = employees;


            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerEditModel input)
        {
            var owner = await this.userManager.GetUserAsync(this.User);
            if (owner.Id != input.OwnerId)
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

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return this.RedirectToAction("Index");
        }

        public async Task<PartialViewResult> EmployerPartial(int employerId)
        {
            var viewModel = await this.employeesManagerService.GetEmployerAsync<EmployerShortInfoViewModel>(employerId);
            return this.PartialView("_ShortEmployerPartial", viewModel);
        }
    }
}
