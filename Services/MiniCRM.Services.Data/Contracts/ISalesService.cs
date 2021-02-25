namespace MiniCRM.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using MiniCRM.Web.ViewModels.Sales;

    public interface ISalesService
    {
        Task AddSaleAsync(SaleCreateModel input, int employerId);
    }
}
