using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MiniCRM.Data.Models;
using MiniCRM.Services.Mapping;

namespace MiniCRM.Web.ViewModels.Sales
{
    public class SaleProductsViewModel : IMapFrom<SaleProduct>, IHaveCustomMappings
    {
        public string ProductName { get; set; }

        public string Quantity { get; set; }

        public decimal Benefit { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SaleProduct, SaleProductsViewModel>()
                .ForMember(x => x.Benefit, options =>
                {
                    options.MapFrom(b => b.Product.Price * b.Quantity);
                });
        }
    }
}
