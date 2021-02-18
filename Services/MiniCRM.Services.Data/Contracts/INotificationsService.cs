using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniCRM.Services.Data.Contracts
{
    public interface INotificationsService
    {

        Task<int> GetNewNotificationsCountAsync(string userId);
        Task CreateNotificationAsync(string userId, string content);

        Task<IEnumerable<T>> GetNotificationsAsync<T>(string userId);
    }
}
