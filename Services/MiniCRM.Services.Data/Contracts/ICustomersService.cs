using System.Linq;

namespace MiniCRM.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using MiniCRM.Web.ViewModels.Customer;

    public interface ICustomersService
    {
        Task<int> CreateAsync(CustomerCreateModel input, string ownerId);

        IQueryable<T> GetAll<T>(string ownerId);

        IQueryable<T> GetEmployerCustomers<T>(string ownerId, int? employerId);

        Task<T> GetByIdAsync<T>(int id);

        Task DeleteAsync(int id);
        public Task UpdateAsync(CustomerEditModel input);

    }
}
