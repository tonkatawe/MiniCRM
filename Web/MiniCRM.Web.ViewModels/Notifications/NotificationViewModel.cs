using System.Globalization;

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

        public string TimeSince => GenerateTimeSince(this.CreatedOn);

        private static string GenerateTimeSince(DateTime datetime)
        {
            string result;
            var totalMinutes = DateTime.UtcNow.Subtract(datetime).TotalMinutes;
            if (totalMinutes >= 60)
            {
                result = totalMinutes >= 1440
                    ? $"{DateTime.UtcNow.Subtract(datetime).TotalDays:f0} days ago"
                    : $"{DateTime.UtcNow.Subtract(datetime).TotalHours:f0} hours ago";
            }
            else
            {
                result = totalMinutes < 1
                    ? $"{DateTime.UtcNow.Subtract(datetime).TotalSeconds:f0} seconds ago"
                    : $"{totalMinutes.ToString(CultureInfo.InvariantCulture):f0} minutes ago";
            }

            return result;
        }
    }
}
