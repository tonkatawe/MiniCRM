namespace MiniCRM.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INotificationsService
    {
        Task<int> GetNewNotificationsCountAsync(string userId);

        Task CreateNotificationAsync(string userId, string content);

        Task<IEnumerable<T>> GetNotificationsAsync<T>(string userId);

        Task<IEnumerable<T>> GetFilteredNotificationsAsync<T>(string userId, bool isRead);

        Task<T> GetByIdAsync<T>(int id);

        Task ReadNotificationsAsync(int notificationId);

        Task DeleteNotificationAsync(int notificationId);
    }
}
