namespace MiniCRM.Web.Areas.Owners.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Common;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Services.Messaging;
    using MiniCRM.Web.Infrastructure;
    using MiniCRM.Web.ViewModels;
    using MiniCRM.Web.ViewModels.Employees;

    public class EmployeesManagerController : OwnersController
    {
        private readonly IEmailSender emailSender;
        private readonly IUsersService usersService;

        public EmployeesManagerController(
            IEmailSender emailSender,
            IUsersService usersService)
        {
            this.emailSender = emailSender;
            this.usersService = usersService;
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
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateModel input)
        {
            var ownerId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var owner = await this.usersService.GetUserAsync<UserViewModel>(ownerId);

            var result = (string.Empty, string.Empty, string.Empty);

            try
            {
                result = await this.usersService.CreateAsync(input, owner);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, "Account doesn't create - " + e.Message);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var confirmationLink = this.Url.Action("ConfirmEmail", "Home", new { area = string.Empty, token = result.Item1, email = input.Email }, this.Request.Scheme);

            var msg = string.Format(OutputMessages.EmailConformation, input.FirstName, owner.FullName, owner.JobTitleName, owner.CompanyName, result.Item3, result.Item2, confirmationLink);

            // TODO uncomment in production!
            // await this.emailSender.SendEmailAsync(owner.Email, owner.FullName, input.Email, $"Email confirm link", msg);

            return this.RedirectToAction("Index");
        }
    }
}
