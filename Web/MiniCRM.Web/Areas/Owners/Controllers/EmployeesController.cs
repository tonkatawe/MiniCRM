using MiniCRM.Web.ViewModels.Employees;

namespace MiniCRM.Web.Areas.Owners.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class EmployeesController : OwnersController
    {
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
            return this.View();
        }
    }
}
