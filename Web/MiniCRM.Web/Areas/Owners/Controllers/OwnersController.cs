namespace MiniCRM.Web.Areas.Owners.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Common;
    using MiniCRM.Web.Controllers;

    [Authorize(Roles = GlobalConstants.OwnerUserRoleName)]
    [Area("Owners")]
    public class OwnersController : BaseController
    {
    }
}
