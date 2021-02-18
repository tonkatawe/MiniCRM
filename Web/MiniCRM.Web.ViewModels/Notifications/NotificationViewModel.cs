namespace MiniCRM.Web.ViewModels.Notifications
{
    using System;

    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class NotificationViewModel : IMapFrom<Notification>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsRead { get; set; }
    }
}
