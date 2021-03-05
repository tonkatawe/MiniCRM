using Microsoft.AspNetCore.Mvc;

namespace MiniCRM.Web.ViewModels.Sales
{
    using System.Collections.Generic;

    using MiniCRM.Web.ViewModels.Products;

    public class SaleCreateModel
    {
        public int CustomerId { get; set; }
        public IList<SaleProductCreateModel> Products { get; set; }
    }
}
