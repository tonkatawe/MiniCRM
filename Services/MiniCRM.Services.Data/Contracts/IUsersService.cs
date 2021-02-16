namespace MiniCRM.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using MiniCRM.Web.ViewModels;
    using MiniCRM.Web.ViewModels.Users;

    public interface IUsersService
    {
        Task<T> GetUserAsync<T>(string userId);

        Task<(string, string, string, string)> CreateAsync(UserCreateModel input, UserViewModel parent, string role);

        IQueryable<T> GetAllUser<T>(string userId);

        Task<int> DeleteAsync(string userId);

        Task ChangeUserEmail(string email, string accountId);
        Task ChangeUserPhoneNumber(string phoneNumber, string accountId);
    }
}
