using MiniCRM.Data.Models;
using MiniCRM.Services.Mapping;

namespace MiniCRM.Web.ViewModels.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc;

    public class UserCreateModel : IMapFrom<Employer>, IMapFrom<Data.Models.Customer>
    {
        [Required]
        public string CompanyId { get; set; }

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

        [Required]
        [MaxLength(30, ErrorMessage = "Job title should be maximum 30 letters")]
        public string JobTitle { get; set; }

        [Required]
        [Remote(action: "VerifyEmail", controller: "Validation", areaName: "")]
        public string Email { get; set; }

        [Required]
        [Remote(action: "VerifyPhone", controller: "Validation", areaName: "")]
        public string Phone { get; set; }
    }
}
