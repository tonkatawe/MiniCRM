using System.Collections.Generic;
using System.Threading.Tasks;
using MiniCRM.Web.ViewModels.Employees;

namespace MiniCRM.Services.Data.Contracts
{
    public interface IEmployeesManagerService
    {
        Task<KeyValuePair<string, string>> CreateAsync(EmployeeCreateModel input, string ownerId);
    }
}
