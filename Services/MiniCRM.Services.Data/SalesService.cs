namespace MiniCRM.Services.Data
{
    using System.Threading.Tasks;

    using MiniCRM.Data.Common.Repositories;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Web.ViewModels.Sales;

    public class SalesService : ISalesService
    {
        private readonly IDeletableEntityRepository<Sale> salesRepository;

        public SalesService(IDeletableEntityRepository<Sale> salesRepository)
        {
            this.salesRepository = salesRepository;
        }

        public async Task AddSaleAsync(SaleCreateModel input, int employerId)
        {
            var sale = new Sale
            {
                CustomerId = input.CustomerId,
                EmployerId = employerId,
            };

            foreach (var product in input.Products)
            {
                sale.Products.Add(new SaleProduct
                {
                    ProductId = product.Id,
                    Quantity = product.Quantity,
                });
            }

            await this.salesRepository.AddAsync(sale);
            await this.salesRepository.SaveChangesAsync();

        }
    }
}
