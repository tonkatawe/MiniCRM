namespace MiniCRM.Web.Areas.Owners.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using MiniCRM.Web.Areas.Owner.Controllers;

    public class OrganizationsController : OwnersController
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
