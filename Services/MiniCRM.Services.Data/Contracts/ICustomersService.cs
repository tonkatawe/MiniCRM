namespace MiniCRM.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using MiniCRM.Web.ViewModels.Customer;

    public interface ICustomersService
    {
        Task<int> CreateAsync(CustomerCreateModel input, string ownerId);
    }
}
