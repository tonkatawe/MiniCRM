﻿using MiniCRM.Web.ViewModels.Attributes;

namespace MiniCRM.Web.ViewModels.Products
{
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;
    using MiniCRM.Web.Infrastructure;

    public class SaleProductCreateModel : IMapFrom<Product>
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        [ProductsQuantity]
        public int SaleProductQuantity { get; set; }
    }
}
