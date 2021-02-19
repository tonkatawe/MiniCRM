using System;
using System.Collections.Generic;
using System.Text;
using MiniCRM.Data.Common.Models;

namespace MiniCRM.Data.Models
{
    public class Sale : BaseDeletableModel<int>
    {
        public Sale()
        {
            this.Products = new HashSet<Product>();
        }
        public int EmployerId { get; set; }

        public virtual Employer Employer { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
