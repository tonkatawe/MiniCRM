using MiniCRM.Services.Data.Contracts;

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
        private readonly IRolesService rolesService;

        public RolesController(RoleManager<ApplicationRole> roleManager, IRolesService rolesService)
        {
            this.roleManager = roleManager;
            this.rolesService = rolesService;
        }

        public async Task<IActionResult> Index()
        {
           // var roles = await this.roleManager.Roles.ToListAsync();
           var roles = await this.rolesService.GetAllAsync();
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
