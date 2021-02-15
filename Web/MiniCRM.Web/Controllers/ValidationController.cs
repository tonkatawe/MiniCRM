namespace MiniCRM.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Data.Models;

    public class ValidationController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ValidationController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [AcceptVerbs("GET", "POST")]
        public JsonResult VerifyEmail(string email)
        {
            if (this.userManager.Users.Any(x => x.Email == email))
            {
                return this.Json($"Email {email} is already in use.");
            }

            return this.Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public JsonResult VerifyPhone(string phone)
        {
            if (this.userManager.Users.Any(x => x.PhoneNumber == phone))
            {
                return Json($"PhoneNumber {phone} is already in use.");
            }

            return this.Json(true);
        }
    }
}
