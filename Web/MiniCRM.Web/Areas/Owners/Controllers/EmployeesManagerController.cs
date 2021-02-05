using MiniCRM.Common;
using MiniCRM.Web.ViewModels.Users;

namespace MiniCRM.Web.Areas.Owners.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Services.Messaging;
    using MiniCRM.Web.Infrastructure;
    using MiniCRM.Web.ViewModels;
    using MiniCRM.Web.ViewModels.Employees;

    public class EmployeesManagerController : OwnersController
    {
        private readonly IEmailSender emailSender;
        private readonly IUsersService usersService;
        private readonly IEmployeesManagerService employeesManagerService;
        private readonly UserManager<ApplicationUser> userManager;

        public EmployeesManagerController(
            IEmailSender emailSender,
            IUsersService usersService,
            IEmployeesManagerService employeesManagerService,
            UserManager<ApplicationUser> userManager)
        {
            this.emailSender = emailSender;
            this.usersService = usersService;
            this.employeesManagerService = employeesManagerService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var ownerId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var owner = await this.usersService.GetUserAsync<UserViewModel>(ownerId);

            if (owner.CompanyId == null)
            {
                return this.RedirectToAction("Create", "Companies", new { area = "Owners" });
            }

            var allEmployers = this.employeesManagerService.GetAll<EmployerViewModel>(owner.CompanyId);

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

            var employees = from c in allEmployers
                            select c;
            if (!string.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.LastName.Contains(searchString)
                                                 || s.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nameDesc":
                    employees = employees
                        .OrderByDescending(e => e.FirstName)
                        .ThenByDescending(e => e.LastName)
                        .ThenByDescending(e => e.MiddleName);
                    break;

                case "jobDesc":
                    employees = employees.OrderByDescending(e => e.JobTitleName);
                    break;
                case "emailDesc":
                    employees = employees.OrderByDescending(e => e.Email);
                    break;
                case "phoneDesc":
                    employees = employees.OrderByDescending(e => e.PhoneNumber);
                    break;
                case "customerCount":
                    // employees = employees.OrderByDescending(e => e.customersCount);
                    break;
                default:
                    employees = employees.OrderBy(c => c.LastName);
                    break;
            }

            int pageSize = 3;

            return this.View(await PaginatedList<EmployerViewModel>.CreateAsync(employees, pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Create()
        {
            var ownerId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var owner = await this.usersService.GetUserAsync<UserViewModel>(ownerId);

            if (owner.CompanyId == null)
            {
                return this.RedirectToAction("Create", "Companies", new { area = "Owners" });
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployerCreateModel input)
        {
            var ownerId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //TODO CHEK FOR EmployersCount! And add it to userModel
            //TODO clear try catch and make validation attributes !!! or blazor validation
            var owner = await this.usersService.GetUserAsync<UserViewModel>(ownerId);

            if (owner.CompanyId == null)
            {
                return this.RedirectToAction("Create", "Companies", new { area = "Owners" });
            }

            try
            {
                await this.employeesManagerService.CreateAsync(input, owner.CompanyId);
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
        public async Task<IActionResult> Delete(int id)
        {
            var employer = await this.employeesManagerService.GetEmployerAsync<EmployerViewModel>(id);

            var ownerId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var owner = await this.usersService.GetUserAsync<UserViewModel>(ownerId);

            if (owner.CompanyId == null)
            {
                return this.RedirectToAction("Create", "Companies", new { area = "Owners" });
            }

            if (employer.CompanyId != owner.CompanyId)
            {
                return this.NotFound();
            }

            if (employer.HasAccount)
            {
                await this.usersService.DeleteAsync(employer.UserId);
            }

            await this.employeesManagerService.DeleteAsync(id);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(int id)
        {
            var ownerId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var owner = await this.usersService.GetUserAsync<UserViewModel>(ownerId);

            if (owner.CompanyId == null)
            {
                return this.RedirectToAction("Create", "Companies", new { area = "Owners" });
            }

            var employer = await this.employeesManagerService.GetEmployerAsync<UserCreateModel>(id);

            if (employer.CompanyId != owner.CompanyId)
            {
                return this.NotFound();
            }

            var result = (string.Empty, string.Empty, string.Empty, string.Empty);

            try
            {
                result = await this.usersService.CreateAsync(employer, owner, GlobalConstants.EmployerUserRoleName);
            }
            catch (Exception e)
            {
                this.TempData["Error"] = "Account doesn't create - " + e.Message;

                return this.RedirectToAction("Index");
            }

            await this.employeesManagerService.ChangeAccountStatusAsync(id, true, result.Item4);

            var confirmationLink = this.Url.Action("ConfirmEmail", "Home", new { area = string.Empty, token = result.Item1, email = employer.Email }, this.Request.Scheme);

            var msg = string.Format(OutputMessages.EmailConformation, employer.FirstName, owner.FullName, owner.JobTitleName, owner.CompanyName, result.Item3, result.Item2, confirmationLink);

            // TODO uncomment in production!
            // await this.emailSender.SendEmailAsync(owner.Email, owner.FullName, input.Email, $"Email confirm link", msg);

            this.TempData["Successful"] = "Account created successful.";

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(string userId, int id)
        {
            var ownerId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await this.usersService.GetUserAsync<UserViewModel>(userId);

            if (user.ParentId != ownerId || userId == null)
            {
                return this.NotFound();
            }

            await this.usersService.DeleteAsync(userId);

            await this.employeesManagerService.ChangeAccountStatusAsync(id, false, null);

            return this.RedirectToAction("Index");
        }
    }
}
