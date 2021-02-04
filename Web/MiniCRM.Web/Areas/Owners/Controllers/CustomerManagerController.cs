using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MiniCRM.Web.Areas.Owners.Controllers
{
    public class CustomerManagerController : OwnersController
    {
        public async Task<IActionResult> Create()
        {
            return this.View();
        }
    }
}
