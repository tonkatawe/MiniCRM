namespace MiniCRM.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using MiniCRM.Web.ViewModels.Employees;

    public interface IEmployeesManagerService
    {
        Task<int> CreateAsync(EmployerCreateModel input);

        Task<T> GetEmployerAsync<T>(int id);

        IQueryable<T> GetAll<T>(string companyId);

        Task ChangeAccountStatusAsync(int id, bool hasAccount, string accountId);

        Task DeleteAsync(int id);

        Task<T> GetByIdAsync<T>(int employerId);

        Task<int> UpdateAsync(EmployerEditModel input);

        Task<int> GetEmployersIdAsync(string accountId);
    }
}
