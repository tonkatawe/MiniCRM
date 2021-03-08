using System.Collections.Generic;
using MiniCRM.Services.Data.Contracts;
using MiniCRM.Web.ViewModels.Products;

namespace MiniCRM.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Data.Models;

    public class ValidationController : Controller
    {
        private readonly IValidationsService validationsService;

        public ValidationController(IValidationsService validationsService)
        {
            this.validationsService = validationsService;
        }

        [AcceptVerbs("GET", "POST")]
        public JsonResult VerifyEmail(string email)
        {
            if (this.validationsService.IsExistUserEmail(email))
            {
                return this.Json($"Email {email} is already in use.");
            }

            return this.Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public JsonResult VerifyPhone(string phone)
        {
            if (this.validationsService.IsExistUserPhoneNumber(phone))
            {
                return Json($"PhoneNumber {phone} is already in use.");
            }

            return this.Json(true);
        }
    }
}
