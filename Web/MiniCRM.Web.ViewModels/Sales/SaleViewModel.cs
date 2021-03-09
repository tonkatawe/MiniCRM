namespace MiniCRM.Web.ViewModels.Sales
{
    using System;
    using System.Collections.Generic;

    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class SaleViewModel : IMapFrom<Sale>
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public IEnumerable<SaleProductsViewModel> Products { get; set; }
    }
}
