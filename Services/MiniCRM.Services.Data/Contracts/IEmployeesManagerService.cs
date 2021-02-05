using System.Linq;

namespace MiniCRM.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MiniCRM.Web.ViewModels.Employees;

    public interface IEmployeesManagerService
    {
        Task<int> CreateAsync(EmployerCreateModel input, string companyId);

        Task<T> GetEmployerAsync<T>(int id);
        IQueryable<T> GetAll<T>(string companyId);

        Task ChangeAccountStatusAsync(int id, bool hasAccount, string userId);
    }
}
