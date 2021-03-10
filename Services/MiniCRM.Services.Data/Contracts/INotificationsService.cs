namespace MiniCRM.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INotificationsService
    {
        Task<int> GetNewNotificationsCountAsync(string userId);

        Task CreateNotificationAsync(string userId, string content);

        Task<ICollection<T>> GetNotificationsAsync<T>(string userId);

        Task<T> GetByIdAsync<T>(int id);

        Task ReadNotificationsAsync(int notificationId);

        Task DeleteNotificationAsync(int notificationId);
    }
}
