using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniCRM.Web.Areas.Owner.Controllers;

namespace MiniCRM.Web.Areas.Owners.Controllers
{
    public class DashboardController : OwnersController
    {
        public async Task<IActionResult> Index()
        {
            
            return this.View();
        }
    }
}
