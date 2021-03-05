namespace MiniCRM.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Web.Infrastructure;
    using MiniCRM.Web.ViewModels.Products;

    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController(IProductsService productsService, UserManager<ApplicationUser> userManager)
        {
            this.productsService = productsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {

            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = user.CompanyId;

            var allProducts = this.productsService.GetAll<ProductViewModel>(companyId);

            var products = from c in allProducts
                           select c;

            var pageSize = 3;

            return this.View(await PaginatedList<ProductViewModel>.CreateAsync(products, pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = "Owner, Administrator")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Owner, Administrator")]
        public async Task<IActionResult> Create(ProductCreateModel input)
        {
            var owner = await this.userManager.GetUserAsync(this.User);
            var companyId = owner.CompanyId;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.productsService.CreateAsync(input, companyId);

            this.TempData["Message"] = "Product added successfully";
            return this.RedirectToAction("Index");

        }

        [Authorize(Roles = "Owner, Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.productsService.GetByIdAsync<EditProductModel>(id);

            return this.View(viewModel);
        }


        [HttpPost]
        [Authorize(Roles = "Owner, Administrator")]
        public async Task<IActionResult> Edit(EditProductModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.productsService.UpdateAsync(input);

            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = "Owner, Administrator")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            //todo campare product companyId with the owner company id
            await this.productsService.DeleteAsync(id);
            return this.RedirectToAction("Index");
        }
    }
}
