using MiniCRM.Web.ViewModels.Products;

namespace MiniCRM.Web.ViewModels.Sales
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SaleCreateModel
    {

        public int CustomerId { get; set; }

        public int EmployerId { get; set; }

        public IList<SaleProductCreateModel> Products { get; set; }
    }
}
