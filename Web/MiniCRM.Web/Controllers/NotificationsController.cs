using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MiniCRM.Services.Data.Contracts;
using MiniCRM.Web.ViewModels.Notifications;

namespace MiniCRM.Web.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly INotificationsService notificationsService;

        public NotificationsController(INotificationsService notificationsService)
        {
            this.notificationsService = notificationsService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var viewModel = new IndexViewModel();

            if (id != null)
            {
                var notificaiton = await this.notificationsService.GetByIdAsync<NotificationViewModel>(id);
                viewModel.Notifications.Add(notificaiton);
                return this.View(viewModel);
            }

            return this.View();
        }

        [HttpPost]
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
    }
}
