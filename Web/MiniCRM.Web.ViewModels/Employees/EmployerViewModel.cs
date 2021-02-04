namespace MiniCRM.Web.ViewModels.Employees
{
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class EmployerViewModel : IMapFrom<Employer>
    {
        public int Id { get; set; }

        public string CompanyId { get; set; }

        public string JobTitleName { get; set; }

        public string FullName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string HasAccount { get; set; }
    }
}
