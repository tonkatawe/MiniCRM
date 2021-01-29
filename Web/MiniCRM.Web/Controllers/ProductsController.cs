using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCRM.Web.Infrastructure;
using MiniCRM.Web.ViewModels.Products;

namespace MiniCRM.Web.Controllers
{
    public class ProductsController:Controller
    {
        public ProductsController()
        {

        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            return View(await PaginatedList<ProductViewModel>.CreateAsync(products, pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = ("Owner, Administrator"))]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = ("Owner, Administrator"))]
        public async Task<IActionResult> Create(ProductCreateModel input)
        {

        }
    }
}
