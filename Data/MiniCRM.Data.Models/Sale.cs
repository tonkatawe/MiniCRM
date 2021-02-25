namespace MiniCRM.Data.Models
{
    using System.Collections.Generic;
    using MiniCRM.Data.Common.Models;

    public class Sale : BaseDeletableModel<int>
    {
        public Sale()
        {
            this.Products = new HashSet<SaleProduct>();
        }

        public int EmployerId { get; set; }

        public virtual Employer Employer { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<SaleProduct> Products { get; set; }
    }
}
