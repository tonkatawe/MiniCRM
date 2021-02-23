using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MiniCRM.Web.Areas.Employees.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Web.ViewModels.Products;
    using MiniCRM.Web.ViewModels.Sales;

    public class SalesController : EmployeesController
    {
        private readonly ISalesService salesService;
        private readonly INotificationsService notificationsService;
        private readonly IProductsService productsService;
        private readonly UserManager<ApplicationUser> userManager;

        public SalesController(
            ISalesService salesService,
            INotificationsService notificationsService,
            IProductsService productsService,
            UserManager<ApplicationUser> userManager)
        {
            this.salesService = salesService;
            this.notificationsService = notificationsService;
            this.productsService = productsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create(int customerId)
        {


            var employerAccount = await this.userManager.GetUserAsync(this.User);
            var products = this.productsService.GetAll<ProductNameAndIdViewModel>(employerAccount.CompanyId).ToList();
            var viewModel = new SaleCreateModel
            {
                Products = products,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaleCreateModel input)
        {
            var employerAccount = await this.userManager.GetUserAsync(this.User);

            return this.View();
        }

        public async Task<PartialViewResult> SaleProductPartial(int productId)
        {
            var viewModel = this.productsService.GetById<ProductViewModel>(productId);
            return this.PartialView("_SaleProductPartial", viewModel);
        }

        public ActionResult GetPartial(int productId)
        {
            var viewModel = this.productsService.GetById<ProductViewModel>(productId);
            return PartialView("_SaleProductPartial", viewModel);
        }
    }
}
