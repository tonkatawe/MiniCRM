namespace MiniCRM.Web.Areas.Employees.Controllers
{
    using System.Collections.Generic;
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
            this.employeesManagerService = employeesManagerService;
            this.userManager = userManager;
        }

        public IActionResult Create(int customerId)
        {
            this.ViewData["CustomerId"] = customerId;
            return this.View(this.ViewData["CustomerId"]);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IList<SaleProductCreateModel> input, int customerId)
        {

            var employerAccount = await this.userManager.GetUserAsync(this.User);
            var employerId = await this.employeesManagerService.GetEmployersIdAsync(employerAccount.Id);

            if (!this.ModelState.IsValid)
            {
                return this.PartialView("_SaleProductPartial", input);
            }

            await this.salesService.AddSaleAsync(input, employerId, customerId);

            await this.notificationsService.CreateNotificationAsync(
                employerAccount.ParentId,
                $"{employerAccount.FullName} added sales");

            return this.Json(new { result = "Success", url = this.Url.Action("Index", "Customers") });
        }

        [HttpPost]
        public async Task<IActionResult> SaleProductPartial(IList<int> ids)
        {
            if (ids.Count == 0)
            {
                return this.Json("You have to choose minimum one product");
            }

            var products = await this.salesService.GetAllProductsSaleAsync<SaleProductCreateModel>(ids);
            return this.PartialView("_SaleProductPartial", products);
        }
    }
}
