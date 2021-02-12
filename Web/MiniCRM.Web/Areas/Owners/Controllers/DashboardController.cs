using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MiniCRM.Data.Models;
using MiniCRM.Services.Data.Contracts;
using MiniCRM.Web.ViewModels.Owners;

namespace MiniCRM.Web.Areas.Owners.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : OwnersController
    {
        private readonly IUsersService usersService;

        public DashboardController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public async Task<IActionResult> Index()
        {
            var ownerId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var viewModel = await this.usersService.GetUserAsync<OwnersDashBoardViewModel>(ownerId);

            return this.View();
        }
    }
}
