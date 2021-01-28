namespace MiniCRM.Web.ViewModels.Employees
{
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class EmployerViewModel : IMapFrom<ApplicationUser>
    {
        public ApplicationUser Parent { get; set; }
    }
}
