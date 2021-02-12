namespace MiniCRM.Web.Areas.Owners.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Web.ViewModels.Owners;

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

            return this.View(viewModel);
        }
    }
}
