using Microsoft.AspNetCore.Identity;
using MiniCRM.Data.Models;
using MiniCRM.Services.Data.Contracts;
using MiniCRM.Web.ViewModels.Products;

namespace MiniCRM.Web.ViewComponent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Web.ViewModels.Notifications;
    [ViewComponent(Name = "ProductsList")]
    public class ProductsListViewComponent : ViewComponent
    {
        private readonly IProductsService productsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsListViewComponent(IProductsService productsService, UserManager<ApplicationUser> userManager)
        {
            this.productsService = productsService;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var employerAccount = await this.userManager.GetUserAsync((ClaimsPrincipal)this.User);
            var products = this.productsService.GetAll<ProductNameAndIdViewModel>(employerAccount.CompanyId).ToList();
            var viewModel = new ProductsListViewModel
            {
                Products = products,
            };
            return this.View(viewModel);
        }
    }
}
