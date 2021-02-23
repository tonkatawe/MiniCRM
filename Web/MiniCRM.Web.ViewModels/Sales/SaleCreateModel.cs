using MiniCRM.Web.ViewModels.Products;

namespace MiniCRM.Web.ViewModels.Sales
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SaleCreateModel
    {
        public SaleCreateModel()
        {
            this.Products = new HashSet<ProductNameAndIdViewModel>();
        }

        public int CustomerId { get; set; }

        public int EmployerId { get; set; }

        public virtual IEnumerable<ProductNameAndIdViewModel> Products { get; set; }
    }
}
