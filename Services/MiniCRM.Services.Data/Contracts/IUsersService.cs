using MiniCRM.Web.ViewModels.Users;

namespace MiniCRM.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using MiniCRM.Web.ViewModels;
    using MiniCRM.Web.ViewModels.Employees;

    public interface IUsersService
    {
        Task<T> GetUserAsync<T>(string userId);

        Task<(string, string, string)> CreateAsync(UserCreateModel input, UserViewModel parent);

        IQueryable<T> GetAllUser<T>(string userId);

        Task<int> DeleteAsync(string userId);
    }
}
