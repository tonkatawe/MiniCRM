using System;
using System.Collections.Generic;
using System.Text;

namespace MiniCRM.Web.ViewModels.Notifications
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.Notifications = new HashSet<NotificationViewModel>();
        }
        public ICollection<NotificationViewModel> Notifications { get; set; }
    }
}
