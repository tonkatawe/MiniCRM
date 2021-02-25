namespace MiniCRM.Web.ViewModels.Sales
{
    using System.Collections.Generic;

    using MiniCRM.Web.ViewModels.Products;

    public class IndexSalesCreateViewModel
    {
        public virtual IEnumerable<ProductNameAndIdViewModel> ProductsList { get; set; }

        public List<int> SelectedIDs { get; set; }

        public SaleCreateModel Sale { get; set; }
    }
}
