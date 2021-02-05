namespace MiniCRM.Web.ViewModels.Employees
{

    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class EmployerShortInfoViewModel : IMapFrom<Employer>
    {
        public string FullName { get; set; }

        public string JobTitleName { get; set; }

        public int CustomersCount { get; set; }
    }
}
