namespace MiniCRM.Web.ViewModels.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.Notifications = new HashSet<NotificationViewModel>();
        }

        public IEnumerable<NotificationViewModel> Notifications { get; set; }
    }
}
