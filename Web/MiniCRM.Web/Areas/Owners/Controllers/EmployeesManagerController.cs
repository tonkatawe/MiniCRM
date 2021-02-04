using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MiniCRM.Web.Areas.Owners.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Common;
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

            var allEmployers = this.usersService.GetAllUser<UserViewModel>(ownerId);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["SortByName"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var employees = from c in allEmployers
                            select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.LastName.Contains(searchString)
                                                 || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {

                case "name_desc":
                    employees = employees
                        .OrderByDescending(c => c.FirstName)
                        .ThenByDescending(c => c.LastName)
                        .ThenByDescending(c => c.MiddleName);
                    break;

                case "Date":
                    //       employees = employees.OrderBy(c => c.OrdersCount);
                    break;
                case "date_desc":
                    //    employees = employees.OrderByDescending(c => c.OrdersCount);
                    break;
                default:
                    employees = employees.OrderBy(c => c.LastName);
                    break;
            }

            int pageSize = 3;

            return View(await PaginatedList<UserViewModel>.CreateAsync(employees, pageNumber ?? 1, pageSize));

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

            //    var result = (string.Empty, string.Empty, string.Empty);



            //    try
            //    {
            //        result = await this.usersService.CreateAsync(input, owner);
            //    }
            //    catch (Exception e)
            //    {
            //        this.ModelState.AddModelError(string.Empty, "Account doesn't create - " + e.Message);
            //    }

            //    if (!this.ModelState.IsValid)
            //    {
            //        return this.View(input);
            //    }

            //    var confirmationLink = this.Url.Action("ConfirmEmail", "Home", new { area = string.Empty, token = result.Item1, email = input.Email }, this.Request.Scheme);

            //    var msg = string.Format(OutputMessages.EmailConformation, input.FirstName, owner.FullName, owner.JobTitleName, owner.CompanyName, result.Item3, result.Item2, confirmationLink);

            //    // TODO uncomment in production!
            //    // await this.emailSender.SendEmailAsync(owner.Email, owner.FullName, input.Email, $"Email confirm link", msg);

            //    return this.RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var ownerId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await this.usersService.GetUserAsync<UserViewModel>(id);

            if (user.ParentId != ownerId || id == null)
            {
                return this.NotFound();
            }

            await this.usersService.DeleteAsync(id);

            return this.RedirectToAction("Index");
        }
    }
}
