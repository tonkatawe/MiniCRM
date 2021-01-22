using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniCRM.Web.Areas.Owner.Controllers
{
    [Area("Owner")]
    [Route("owners")]
    public class OwnersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
