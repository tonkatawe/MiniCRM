using AutoMapper;
using AutoMapper.Configuration.Annotations;

namespace MiniCRM.Web.ViewModels.Products
{
    using System.ComponentModel.DataAnnotations.Schema;

    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class SaleProductCreateModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [IgnoreMap]
        public int Quantity { get; set; }
    }
}
