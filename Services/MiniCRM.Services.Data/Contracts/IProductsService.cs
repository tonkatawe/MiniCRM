namespace MiniCRM.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using MiniCRM.Web.ViewModels.Products;

    public interface IProductsService
    {
        Task<int> CreateAsync(ProductCreateModel input, string companyId);

        Task<int> UpdateAsync(EditProductModel input);

        Task<int> DeleteAsync(int productId);

        IQueryable<T> GetAll<T>(string companyId);

        T GetById<T>(int productId);
    }
}
