namespace MiniCRM.Web.Areas.Owners.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Common;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Services.Messaging;
    using MiniCRM.Web.ViewModels;
    using MiniCRM.Web.ViewModels.Employees;
    using MiniCRM.Web.ViewModels.Users;

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

        public IActionResult Index()
        {
            //todo make validaiton for existing employers
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployees()
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

        public async Task<IActionResult> DetailsPartial(int id)
        {
            var owner = await this.userManager.GetUserAsync(this.User);

            var viewModel = await this.employeesManagerService.GetByIdAsync<DetailsEmployerViewModel>(id);

            if (owner.CompanyId != viewModel.CompanyId)
            {
                return this.NotFound();
            }

            return this.PartialView("Details", viewModel);

        }

        public async Task<IActionResult> Create()
        {
            var ownerId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var owner = await this.usersService.GetUserAsync<UserViewModel>(ownerId);

            if (owner.CompanyId == null)
            {
                return this.RedirectToAction("Create", "Companies", new { area = "Owners" });
            }

            var viewModel = new EmployerCreateModel
            {
                OwnerId = ownerId,
                CompanyId = owner.CompanyId,
            };

            return this.View(viewModel);
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
                await this.employeesManagerService.CreateAsync(input);
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
                await this.usersService.DeleteAsync(employer.AccountId);
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

            // TODO uncomment in production! Change GlobalConstants.SystemEmail with owner.Email!!!!
            await this.emailSender.SendEmailAsync(GlobalConstants.SystemEmail, owner.FullName, employer.Email, $"Email confirm link", msg);

            this.TempData["Successful"] = "Account created successful.";

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(string accountId, int id)
        {
            var ownerId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await this.usersService.GetUserAsync<UserViewModel>(accountId);

            if (user.ParentId != ownerId || accountId == null)
            {
                return this.NotFound();
            }

            await this.usersService.DeleteAsync(accountId);

            await this.employeesManagerService.ChangeAccountStatusAsync(id, false, null);

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int employerId)
        {
            var owner = await this.userManager.GetUserAsync(this.User);

            var viewModel = await this.employeesManagerService.GetByIdAsync<DetailsEmployerViewModel>(employerId);

            if (owner.CompanyId != viewModel.CompanyId)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Edit(int employerId)
        {
            var owner = await this.userManager.GetUserAsync(this.User);
            var viewModel = await this.employeesManagerService.GetByIdAsync<EmployerEditModel>(employerId);
            if (owner.CompanyId != viewModel.CompanyId)
            {
                return this.NotFound();
            }

            //viewModel.OwnerId = owner.Id;


            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployerEditModel input)
        {
            var owner = await this.userManager.GetUserAsync(this.User);

            if (owner.CompanyId != input.CompanyId)
            {
                return this.NotFound();
            }



            try
            {
                await this.employeesManagerService.UpdateAsync(input);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return this.RedirectToAction("Details", new { employerId = input.Id });
        }


    }
}
