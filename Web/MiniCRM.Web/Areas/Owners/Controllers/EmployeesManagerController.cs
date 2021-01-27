using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MiniCRM.Common;
using MiniCRM.Data.Models;
using MiniCRM.Services.Data.Contracts;
using MiniCRM.Services.Messaging;

namespace MiniCRM.Web.Areas.Owners.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Web.ViewModels.Employees;

    public class EmployeesManagerController : OwnersController
    {
        private readonly IEmployeesManagerService employeesManagerService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;

        public EmployeesManagerController(IEmployeesManagerService employeesManagerService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            this.employeesManagerService = employeesManagerService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {

            return this.View();
        }

        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateModel input)
        {
            var ownerId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //only for test

            var owner = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var result = new KeyValuePair<string, string>();
            try
            {
                result = await this.employeesManagerService.CreateAsync(input, ownerId);
            }
            catch (Exception e)
            {
                TempData["Error"] = "Account doesn't create - " + e.Message;

                return this.RedirectToAction("Index");
            }


            var confirmationLink = Url.Action("ConfirmEmail", "Home",
               new { area = "", token = result.Key, email = input.Email },
                Request.Scheme);

            var msg = string.Format(OutputMessages.EmailConformation, input.FirstName, owner.CompanyId,
                input.UserName, result.Value, confirmationLink);

            await this.emailSender.SendEmailAsync(GlobalConstants.SystemEmail, owner.UserName,
                input.Email, "Email confirm link", msg);

            return this.RedirectToAction("Index");
        }
        
      
    }
}
