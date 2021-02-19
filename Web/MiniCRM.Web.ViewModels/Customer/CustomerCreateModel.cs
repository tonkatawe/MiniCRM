namespace MiniCRM.Web.ViewModels.Customer
{
    using System.Collections.Generic;

    using MiniCRM.Web.ViewModels.BaseModels;
    using MiniCRM.Web.ViewModels.Employees;

    public class CustomerCreateModel : BaseUnitPersonModel
    {
        public CustomerCreateModel()
        {
            this.Employees = new HashSet<EmployeesDropDownViewModel>();
        }

        public int EmployerId { get; set; }

        public string OwnerId { get; set; }

        public string AdditionalInfo { get; set; }

        public IEnumerable<EmployeesDropDownViewModel> Employees { get; set; }
    }
}
