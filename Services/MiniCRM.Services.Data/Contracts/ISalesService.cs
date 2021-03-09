using System.Linq;

namespace MiniCRM.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MiniCRM.Web.ViewModels.Sales;

    public interface ISalesService
    {
        Task AddSaleAsync(IList<SaleProductCreateModel> input, int employerId, int customerId);

        Task<IList<T>> GetAllProductsSaleAsync<T>(IList<int> ids);

        IQueryable<T> GetAllCustomerOrders<T>(int customerId);

        Task<T> GetSaleById<T>(int saleId);
    }
}
