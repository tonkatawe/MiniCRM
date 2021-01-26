using System.ComponentModel.DataAnnotations;
using MiniCRM.Web.ViewModels.Addresses;
using MiniCRM.Web.ViewModels.Emails;
using MiniCRM.Web.ViewModels.Phones;

namespace MiniCRM.Web.ViewModels.Employees
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EmployeeCreateModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "The first name should be maximum 20 letters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use latin letters only please")]
        public string FirstName { get; set; }

        [MaxLength(20, ErrorMessage = "The middle name should be maximum 20 letters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use latin letters only please")]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The last name should be maximum 20 letters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use latin letters only please")]
        public string LastName { get; set; }

        [MaxLength(30, ErrorMessage = "Job title should be maximum 30 letters")]
        public string JobTitle { get; set; }

        public AddressCreateModel Address { get; set; }

        public IList<EmailCreateModel> Emails { get; set; }

        public IList<PhoneCreateModel> Phones { get; set; }
    }
}
