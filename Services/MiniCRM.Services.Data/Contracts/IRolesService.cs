using System.Collections.Generic;
using System.Threading.Tasks;
using MiniCRM.Data.Models;

namespace MiniCRM.Services.Data.Contracts
{
    public interface IRolesService
    {
        Task<IEnumerable<ApplicationRole>> GetAllAsync();

        Task CreateAsync(string roleName);
    }
}
