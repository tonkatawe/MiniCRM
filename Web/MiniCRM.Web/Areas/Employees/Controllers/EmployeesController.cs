namespace MiniCRM.Web.Areas.Employees.Controllers
{

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Common;
    using MiniCRM.Web.Controllers;

    [Authorize(Roles = GlobalConstants.EmployerUserRoleName)]
    [Area("Employees")]
    public class EmployeesController : BaseController
    {
    }
}
