﻿using System.Security.Claims;

namespace MiniCRM.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Services.Data.Contracts;
    using MiniCRM.Web.ViewModels.Notifications;

    public class NotificationsController : Controller
    {
        private readonly INotificationsService notificationsService;

        public NotificationsController(INotificationsService notificationsService)
        {
            this.notificationsService = notificationsService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var notifications =
                await this.notificationsService.GetNotificationsAsync<NotificationViewModel>(userId);


            var viewModel = new IndexViewModel
            {
                Notifications = notifications,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> SeeAll(string userId)
        {
            if (userId != null)
            {
                var currentUrl = this.HttpContext.Request.GetDisplayUrl();
                var notifications = await this.notificationsService.GetNotificationsAsync<NotificationViewModel>(userId);
                foreach (var notification in notifications)
                {
                    await this.notificationsService.ReadNotificationsAsync(notification.Id);
                }

                return this.Ok(currentUrl);
            }

            return this.Ok();
        }

        [HttpPost]
        public async Task<IActionResult> See(int notificationId, string url)
        {
            var currentUrl = url;

            await this.notificationsService.ReadNotificationsAsync(notificationId);

            return this.Redirect(currentUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int notificationId)
        {
            await this.notificationsService.DeleteNotificationAsync(notificationId);

            return this.Redirect("/Notifications/Index");
        }
    }
}
