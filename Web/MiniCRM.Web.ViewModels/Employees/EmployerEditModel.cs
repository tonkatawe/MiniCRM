namespace MiniCRM.Web.ViewModels.Employees
{
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class EmployerEditModel : EmployerCreateModel, IMapFrom<Employer>
    {
        public int Id { get; set; }

        public string CompanyId { get; set; }
     
    }
}
