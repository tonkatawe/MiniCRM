using System.Collections.Generic;
using MiniCRM.Web.ViewModels.Products;

namespace MiniCRM.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using MiniCRM.Web.ViewModels.Sales;

    public interface ISalesService
    {
        Task AddSaleAsync(SaleCreateModel input, int employerId);

        Task<IList<T>> GetAllProductsSaleAsync<T>(IList<int> ids);
    }
}
