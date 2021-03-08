namespace MiniCRM.Web.ViewModels.Sales
{
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;
    using MiniCRM.Web.ViewModels.Attributes;

    public class SaleProductCreateModel : IMapFrom<Product>
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        [ProductsQuantity]
        public int SaleProductQuantity { get; set; }
    }
}
