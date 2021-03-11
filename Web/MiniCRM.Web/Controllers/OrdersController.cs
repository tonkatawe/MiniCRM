namespace MiniCRM.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Common;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Web.Infrastructure;
    using MiniCRM.Web.ViewModels.Sales;

    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly ISalesService salesService;

        public OrdersController(IOrdersService ordersService, ISalesService salesService)
        {
            this.ordersService = ordersService;
            this.salesService = salesService;
        }

        public async Task<IActionResult> Customer(int customerId, string fullName, string sortOrder, int? pageNumber)
        {
            var allOrders = this.ordersService.GetCustomerOrders<SaleViewModel>(customerId);
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

            return this.View("Orders", await PaginatedList<SaleViewModel>.CreateAsync(orders, pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Employer(int employerId)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = GlobalConstants.OwnerUserRoleName)]
        public async Task<IActionResult> Owner(string sortOrder, int? pageNumber)
        {
            var ownerId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var allOrders = this.ordersService.GetAllOrders<SaleViewModel>(ownerId);

            int pageSize = 3;

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

            return this.View("Orders", await PaginatedList<SaleViewModel>.CreateAsync(orders, pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await this.salesService.GetSaleById<SaleViewModel>(id);
            return this.PartialView("_SaleDetails", viewModel);
        }
    }
}
