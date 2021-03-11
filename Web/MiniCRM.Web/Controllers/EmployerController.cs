namespace MiniCRM.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Data;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Web.ViewModels.Employees;

    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly IEmployeesManagerService employeesManagerService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public EmployerController(
            IEmployeesManagerService employeesManagerService,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            this.employeesManagerService = employeesManagerService;
            this.userManager = userManager;
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomers()
        {
            var owner = await this.userManager.GetUserAsync(this.User);
            var allEmployers = this.employeesManagerService.GetAll<EmployerViewModel>(owner.CompanyId);

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
                var customerData = from tempcustomer in allEmployers select tempcustomer;
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
    }
}
