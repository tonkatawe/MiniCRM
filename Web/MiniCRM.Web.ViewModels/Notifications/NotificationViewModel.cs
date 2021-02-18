namespace MiniCRM.Web.ViewModels.Notifications
{

    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class NotificationViewModel : IMapFrom<Notification>
    {
        public string UserId { get; set; }

        public string Content { get; set; }
        
        public string CreatedOn { get; set; }
    }
}
