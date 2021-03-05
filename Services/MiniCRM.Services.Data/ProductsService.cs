using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

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

        public async Task<int> UpdateAsync(EditProductModel input)
        {
            var product = await this.productsRepository
                .All()
                .Where(x => x.Id == input.Id)
                .FirstOrDefaultAsync();

            product.Name = input.Name;
            product.Description = input.Description;
            product.Price = input.Price;
            product.Quantity = input.Quantity;

            if (input.ProductPicture != null)
            {
                //TODO MAKE DELETE OLD PICTURE FROM CLOUDINARY!!!
                var productPictureUrl = await this.cloudinaryService.UploadAsync(input.ProductPicture, "MiniCRM/ProductPictures");
                product.ProductPictureUrl = productPictureUrl;
            }

            this.productsRepository.Update(product);

            return await this.productsRepository.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int productId)
        {
            var product = await this.productsRepository
                .All()
                .Where(x => x.Id == productId)
                .FirstOrDefaultAsync();
            this.productsRepository.Delete(product);

            //todo MAKE DELETE PICTURE FROM CLOUDINARY
            return await this.productsRepository.SaveChangesAsync();
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

        public async Task<T> GetByIdAsync<T>(int productId)
        {
            var product = await this.productsRepository
                .All()
                .Where(x => x.Id == productId)
                .To<T>()
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task DecreaseQuantityAsync(int productId, int quantity)
        {
            var product = await this.productsRepository
                .All()
                .Where(x => x.Id == productId)
                .FirstOrDefaultAsync();

            product.Quantity -= quantity;

            this.productsRepository.Update(product);
            await this.productsRepository.SaveChangesAsync();
        }
    }
}
