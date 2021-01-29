namespace MiniCRM.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MiniCRM.Data.Common.Repositories;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Contracts;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Services.Mapping;
    using MiniCRM.Web.ViewModels.Products;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly ICloudinaryService cloudinaryService;

        public ProductsService(
            IDeletableEntityRepository<Product> productsRepository,
            ICloudinaryService cloudinaryService)
        {
            this.productsRepository = productsRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<int> CreateAsync(ProductCreateModel input, string companyId)
        {
            var productPictureUrl = await this.cloudinaryService.UploadAsync(input.ProductPicture, "MiniCRM/ProductPictures");

            var product = new Product
            {
                CompanyId = companyId,
                Name = input.Name,
                Price = input.Price,
                Description = input.Description,
                Quantity = input.Quantity,
                ProductPictureUrl = productPictureUrl,
            };

            await this.productsRepository.AddAsync(product);

            return await this.productsRepository.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(int productId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> DeleteAsync(int productId)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<T> GetAll<T>(string companyId)
        {
            var query = this.productsRepository
                .All()
                .Where(x => x.CompanyId == companyId)
                .To<T>()
                .AsQueryable();

            return query;
        }

        public T GetById<T>(int productId)
        {
            var product = this.productsRepository
                .All()
                .Where(x => x.Id == productId)
                .To<T>()
                .FirstOrDefault();

            return product;
        }
    }
}
