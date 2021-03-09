namespace MiniCRM.Web.ViewModels.Customer
{
    using System;
    using System.Collections.Generic;

    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;
    using MiniCRM.Web.ViewModels.Sales;

    public class CustomerOrdersViewModel 
    {
        public string FullName { get; set; }

        public IEnumerable<SaleViewModel> Orders { get; set; }
    }

    //public DateTime CreatedOn { get; set; }

    //public IEnumerable<SaleProductsViewModel> Products { get; set; }
}

