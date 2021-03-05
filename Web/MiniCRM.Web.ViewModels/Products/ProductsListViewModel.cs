namespace MiniCRM.Web.ViewModels.Products
{
    using System.Collections.Generic;

    public class ProductsListViewModel
    {
        public virtual IEnumerable<ProductNameAndIdViewModel> Products { get; set; }

        public List<int> SelectedIDs { get; set; }
    }
}
