namespace MiniCRM.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MiniCRM.Web.ViewModels.Employees;

    public interface IEmployeesManagerService
    {
        Task<int> CreateAsync(EmployerCreateModel input, string companyId);
    }
}
