namespace MiniCRM.Web.ViewModels.Products
{
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class EditProductModel : ProductCreateModel, IMapFrom<Product>
    {
        public int Id { get; set; }

        public string ProductPictureUrl { get; set; }
    }
}
