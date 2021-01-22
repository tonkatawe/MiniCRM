using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MiniCRM.Common;
using MiniCRM.Web.Controllers;

namespace MiniCRM.Web.Areas.Owner.Controllers
{
    [Authorize(Roles = GlobalConstants.OwnerUserRoleName)]
    [Area("Owners")]
    public class OwnersController : BaseController
    {
     }
}
