using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MiniCRM.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MiniCRM.Data.Common.Repositories;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Web.ViewModels.Products;
    using MiniCRM.Web.ViewModels.Sales;

    public class SalesService : ISalesService
    {
        private readonly IDeletableEntityRepository<Sale> salesRepository;
        private readonly IProductsService productsService;

        public SalesService(
            IDeletableEntityRepository<Sale> salesRepository,
            IProductsService productsService)
        {
            this.salesRepository = salesRepository;
            this.productsService = productsService;
        }

        public async Task AddSaleAsync(IList<SaleProductCreateModel> input, int employerId, int customerId)
        {
            var sale = new Sale
            {
                CustomerId = customerId,
                EmployerId = employerId,
            };


            foreach (var product in input)
            {
                await this.productsService.DecreaseQuantityAsync(product.Id, product.SaleProductQuantity);

                sale.Products.Add(new SaleProduct
                {
                    ProductId = product.Id,
                    Quantity = product.SaleProductQuantity,
                });
            }

            await this.salesRepository.AddAsync(sale);
            await this.salesRepository.SaveChangesAsync();

        }

        public async Task<IList<T>> GetAllProductsSaleAsync<T>(IList<int> ids)
        {
            var products = new List<T>();

            foreach (var id in ids)
            {
                products.Add(await this.productsService.GetByIdAsync<T>(id));
            }

            return products;
        }

    
    }
}
