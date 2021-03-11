namespace MiniCRM.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Common;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Web.Infrastructure;
    using MiniCRM.Web.ViewModels.Sales;

    [Authorize]
    public class SalesController : Controller
    {
        private readonly ISalesService salesService;
        private readonly INotificationsService notificationsService;
        private readonly IEmployeesManagerService employeesManagerService;
        private readonly UserManager<ApplicationUser> userManager;

        public SalesController(
            ISalesService salesService,
            INotificationsService notificationsService,
            IEmployeesManagerService employeesManagerService,
            UserManager<ApplicationUser> userManager)
        {
            this.salesService = salesService;
            this.notificationsService = notificationsService;
            this.employeesManagerService = employeesManagerService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> GetOrders(int customerId, string fullName, string sortOrder, int? pageNumber)
        {
            var allOrders = this.salesService.GetAllCustomerOrders<SaleViewModel>(customerId);
            int pageSize = 3;
            this.ViewData["CustomerId"] = customerId;
            this.ViewData["FullName"] = fullName;
            this.ViewData["CurrentSort"] = sortOrder;
            this.ViewData["SortByDate"] = string.IsNullOrEmpty(sortOrder) ? "dateSorted" : string.Empty;
            this.ViewData["Benefit"] = string.IsNullOrEmpty(sortOrder) ? "benefitSorted" : string.Empty;

            var orders = from o in allOrders select o;

            switch (sortOrder)
            {
                case "benefitSorted":
                    orders = orders.OrderByDescending(x => x.Products.Sum(x => x.Benefit));
                    break;
                case "dateSorted":
                    orders = orders.OrderBy(x => x.CreatedOn);
                    break;
                    //case "nameSorted":
                    //    orders = orders.OrderBy(x => x.ProductName);
                    //    break;
            }

            return this.View(await PaginatedList<SaleViewModel>.CreateAsync(orders, pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = GlobalConstants.EmployerUserRoleName)]
        public IActionResult Create(int customerId)
        {
            this.ViewData["CustomerId"] = customerId;
            return this.View(this.ViewData["CustomerId"]);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.EmployerUserRoleName)]

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

            return this.Json(new { result = "Success", url = this.Url.Action("Index", "Customers", new { Area = "Employees" }) });
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.EmployerUserRoleName)]
        public async Task<IActionResult> SaleProductPartial(IList<int> ids)
        {
            if (ids.Count == 0)
            {
                return this.Json("You have to choose minimum one product");
            }

            var products = await this.salesService.GetAllProductsSaleAsync<SaleProductCreateModel>(ids);
            return this.PartialView("_SaleProductPartial", products);
        }

        [Authorize(Roles = GlobalConstants.OwnerUserRoleName)]

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await this.salesService.GetSaleById<SaleViewModel>(id);
            return this.PartialView("_SaleDetails", viewModel);
        }
    }
}
