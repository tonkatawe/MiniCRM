namespace MiniCRM.Web.ViewModels.Employees
{
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class DetailsEmployerViewModel : EmployerViewModel
    {
        public string AddressCountry { get; set; }

        public string AddressCity { get; set; }

        public string AddressStreet { get; set; }

        public string AddressZipCode { get; set; }

    }
}
