using System.Security.Claims;
using System.Threading.Tasks;
using MiniCRM.Services.Data.Contracts;
using MiniCRM.Web.ViewModels.Notifications;

namespace MiniCRM.Web.ViewComponent
{
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "Notifications")]
    public class NotificationsViewComponent : ViewComponent
    {
        private readonly INotificationsService notificationsService;

        public NotificationsViewComponent(INotificationsService notificationsService)
        {
            this.notificationsService = notificationsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var viewModel = await this.notificationsService.GetNotificationsAsync<NotificationViewModel>(userId);
            return this.View(viewModel);
        }
    }
}
