namespace MiniCRM.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MiniCRM.Data.Models;

    public class RolesController : AdministrationController
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public RolesController(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager;

        }

        public async Task<IActionResult> Index()
        {
            var roles = await this.roleManager.Roles.ToListAsync();

            return this.View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> Add(string roleName)
        {
            if (roleName != null)
            {
                await this.roleManager.CreateAsync(new ApplicationRole(roleName.Trim()));
            }

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(string id)
        {
            var role = await this.roleManager.FindByIdAsync(id);

            await this.roleManager.DeleteAsync(role);

            return this.RedirectToAction("Index");
        }
    }
}
