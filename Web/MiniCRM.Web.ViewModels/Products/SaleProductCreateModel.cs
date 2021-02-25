namespace MiniCRM.Web.ViewModels.Products
{
    using AutoMapper;
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
