namespace MiniCRM.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Web.Infrastructure;
    using MiniCRM.Web.ViewModels.Products;

    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {

            var companyId = "0847db38-4e66-4081-b564-6e2ffdf33fc0";


            var allProducts = this.productsService.GetAll<ProductViewModel>(companyId);

            var products = from c in allProducts
                           select c;

            var pageSize = 3;

            return this.View(await PaginatedList<ProductViewModel>.CreateAsync(products, pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = "Owner, Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Owner, Administrator")]
        public async Task<IActionResult> Create(ProductCreateModel input)
        {
            var companyId = "0847db38-4e66-4081-b564-6e2ffdf33fc0";

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.productsService.CreateAsync(input, companyId);

            this.TempData["Message"] = "Product added successfully";
            return this.RedirectToAction("Index");

        }

        [Authorize(Roles = "Owner, Administrator")]
        public IActionResult Edit(int id)
        {
            var viewModel = this.productsService.GetById<EditProductModel>(id);

            return this.View(viewModel);
        }


        [HttpPost]
        [Authorize(Roles = "Owner, Administrator")]
        public async Task<IActionResult> Edit(EditProductModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            //   await this.productsService.UpdateAsync(input);

            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = "Owner, Administrator")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            await this.productsService.DeleteAsync(id);
            return this.RedirectToAction("Index");
        }
    }
}
