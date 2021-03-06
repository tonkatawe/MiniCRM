﻿namespace MiniCRM.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MiniCRM.Data.Common.Repositories;
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Services.Mapping;

    public class NotificationsService : INotificationsService
    {
        private readonly IDeletableEntityRepository<Notification> notificationsRepository;

        public NotificationsService(IDeletableEntityRepository<Notification> notificationsRepository)
        {
            this.notificationsRepository = notificationsRepository;
        }

        public async Task<int> GetNewNotificationsCountAsync(string userId)
        {
            return await this.notificationsRepository
                .All()
                .Where(x => x.UserId == userId && x.IsRead == false)
                .CountAsync();
        }

        public async Task CreateNotificationAsync(string userId, string content)
        {
            var notification = new Notification
            {
                UserId = userId,
                Content = content,
            };

            await this.notificationsRepository.AddAsync(notification);
            await this.notificationsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetNotificationsAsync<T>(string userId)
        {
            return await this.notificationsRepository.All()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .To<T>()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetFilteredNotificationsAsync<T>(string userId, bool isRead)
        {
            return await this.notificationsRepository.All()
                .Where(x => x.UserId == userId && x.IsRead == isRead)
                .OrderByDescending(x => x.CreatedOn)
                .To<T>()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.notificationsRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task ReadNotificationsAsync(int notificationId)
        {
            var notification = await this.notificationsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == notificationId);

            if (notification.IsRead == false)
            {
                notification.IsRead = true;
            }

            this.notificationsRepository.Update(notification);

            await this.notificationsRepository.SaveChangesAsync();
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            var notification = await this.notificationsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == notificationId);

            notification.IsRead = true;

            this.notificationsRepository.Delete(notification);

            await this.notificationsRepository.SaveChangesAsync();
        }
    }
}
