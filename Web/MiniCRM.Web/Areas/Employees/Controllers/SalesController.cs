using System;

namespace MiniCRM.Web.Areas.Employees.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
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
        private readonly IEmployeesManagerService employeesManagerService;
        private readonly UserManager<ApplicationUser> userManager;

        public SalesController(
            ISalesService salesService,
            INotificationsService notificationsService,
            IProductsService productsService,
            IEmployeesManagerService employeesManagerService,
            UserManager<ApplicationUser> userManager)
        {
            this.salesService = salesService;
            this.notificationsService = notificationsService;
            this.productsService = productsService;
            this.employeesManagerService = employeesManagerService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create(int customerId)
        {


            var employerAccount = await this.userManager.GetUserAsync(this.User);
            var products = this.productsService.GetAll<ProductNameAndIdViewModel>(employerAccount.CompanyId).ToList();
            var viewModel = new IndexSalesCreateViewModel
            {
                ProductsList = products,
                Sale = new SaleCreateModel
                {
                    CustomerId = customerId,
                },
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaleCreateModel input)
        {
            var employerAccount = await this.userManager.GetUserAsync(this.User);
            var employerId = await this.employeesManagerService.GetEmployersIdAsync(employerAccount.Id);
            if (!this.ModelState.IsValid)
            {
                return this.PartialView("_SaleProductPartial", input);
            }

            await this.salesService.AddSaleAsync(input, employerId);

            await this.notificationsService.CreateNotificationAsync(employerAccount.ParentId,
                $"{employerAccount.FullName} added sales");

            return this.RedirectToAction("Index", "Customers");
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public PartialViewResult SaleProductPartial(IList<int> ids, int customerId)
        {
            var products = new List<SaleProductCreateModel>();
            foreach (var id in ids)
            {
                var product = this.productsService.GetById<SaleProductCreateModel>(id);
                products.Add(product);
            }

            var viewModel = new SaleCreateModel
            {
                Products = products,
                CustomerId = customerId,
            };
            return this.PartialView("_SaleProductPartial", viewModel);
        }

    }
}
