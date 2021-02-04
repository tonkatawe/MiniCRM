using MiniCRM.Web.ViewModels.Users;

namespace MiniCRM.Web.ViewModels.Employees
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc;

    public class EmployerCreateModel : UserCreateModel
    {
        public string CompanyId { get; set; }
    }
}
