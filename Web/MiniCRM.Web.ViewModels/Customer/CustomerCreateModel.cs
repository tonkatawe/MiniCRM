using System.Collections.Generic;
using MiniCRM.Web.ViewModels.Employees;

namespace MiniCRM.Web.ViewModels.Customer
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc;

    public class CustomerCreateModel : EmployerCreateModel
    {
        public string OwnerId { get; set; }

        public int EmployerId { get; set; }

        public string AdditionalInfo { get; set; }

        public IEnumerable<EmployeesDropDownViewModel> Employees { get; set; }
    }
}
