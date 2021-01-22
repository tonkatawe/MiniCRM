namespace MiniCRM.Data.Models
{
    using System.Collections.Generic;

    using MiniCRM.Data.Common.Models;

    public class Industry : BaseDeletableModel<int>
    {
        public Industry()
        {
            this.Companies = new HashSet<Company>();
        }

        public string Name { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
