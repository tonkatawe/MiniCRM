namespace MiniCRM.Web.ViewModels.Customer
{
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class CustomerEditModel : CustomerCreateModel, IMapFrom<Customer>
    {
        public int Id { get; set; }

    }
}
