namespace MiniCRM.Web.Areas.Owners.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : OwnersController
    {
        public async Task<IActionResult> Index()
        {
            return this.View();
        }
    }
}
