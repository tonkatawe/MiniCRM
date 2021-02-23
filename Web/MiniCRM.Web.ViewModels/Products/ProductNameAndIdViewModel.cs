namespace MiniCRM.Web.ViewModels.Products
{
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class ProductNameAndIdViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
