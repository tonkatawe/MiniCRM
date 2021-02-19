namespace MiniCRM.Web.ViewModels.Employees
{
    using System.ComponentModel.DataAnnotations;

    using MiniCRM.Web.ViewModels.BaseModels;

    public class EmployerCreateModel : BaseUnitPersonModel
    {
        [Required]
        public string OwnerId { get; set; }
    }
}
