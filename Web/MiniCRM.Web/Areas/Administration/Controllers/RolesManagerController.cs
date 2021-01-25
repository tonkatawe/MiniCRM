using MiniCRM.Data.Models;

namespace MiniCRM.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class RolesManagerController : AdministrationController
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public RolesManagerController(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await this.roleManager.Roles.ToListAsync();
            return this.View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await this.roleManager.CreateAsync(new ApplicationRole(roleName.Trim()));
            }

            return this.RedirectToAction("Index");
        }
    }
}
