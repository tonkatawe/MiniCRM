using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Internal;

namespace MiniCRM.Web.ViewModels.Owners
{
    using MiniCRM.Data.Models;
    using MiniCRM.Services.Mapping;

    public class OwnersDashBoardViewModel : IMapFrom<ApplicationUser>
    {
        public string CompanyId { get; set; }
        public int EmployeesCount { get; set; }

        public int CustomersCount { get; set; }

        public string CompanyName { get; set; }

        public int ProductsCount { get; set; }

        public int UsersCount { get; set; }

        public int OrdersCount { get; set; }
   
    }
}
