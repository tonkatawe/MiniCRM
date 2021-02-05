namespace MiniCRM.Web.ViewModels.Employees
{

    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class EmployeesDropDownViewModel : IMapFrom<Employer>
    {
        public int Id { get; set; }

        public string FullName { get; set; }
    }
}
