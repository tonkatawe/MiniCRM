using System.Linq;

namespace MiniCRM.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Data.Models;

    [Authorize]
    public class ClaimsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ClaimsController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public ViewResult Index()
        {
            return View(User?.Claims);
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string claimType, string claimValue)
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            Claim claim = new Claim(claimType, claimValue, ClaimValueTypes.String);
            IdentityResult result = await this.userManager.AddClaimAsync(user, claim);

            if (result.Succeeded)
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                this.Errors(result);
            }

            return this.View();
        }

        [HttpPost]

        public async Task<IActionResult> Delete(string claimValues)
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            string[] claimValuesArray = claimValues.Split(";");
            string claimType = claimValuesArray[0];
            string claimValue = claimValuesArray[1];
            string claimIssuer = claimValuesArray[2];

            Claim claim = this.User
                .Claims
                .FirstOrDefault(x => x.Type == claimType && x.Value == claimValue && x.Issuer == claimIssuer);

            IdentityResult result = await this.userManager.RemoveClaimAsync(user, claim);

            if (result.Succeeded)
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                this.Errors(result);
            }

            return this.View("Index");
        }


        void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                this.ModelState.AddModelError("", error.Description);
        }
    }
}
