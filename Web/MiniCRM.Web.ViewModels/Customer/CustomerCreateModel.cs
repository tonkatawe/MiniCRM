namespace MiniCRM.Web.ViewModels.Customer
{
    using System.Collections.Generic;

    using MiniCRM.Web.ViewModels.Employees;

    public class CustomerCreateModel : EmployerCreateModel
    {
        public string OwnerId { get; set; }

        public int EmployerId { get; set; }

        public string AdditionalInfo { get; set; }

        public IEnumerable<EmployeesDropDownViewModel> Employees { get; set; }
    }
}
