namespace MiniCRM.Web.ViewModels.Employees
{
    using System.ComponentModel.DataAnnotations;

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
        
        [Required]
        [MaxLength(30, ErrorMessage = "Job title should be maximum 30 letters")]
        public string JobTitle { get; set; }

        [Required]
        [MaxLength(30)]
        public string Country { get; set; }
        [Required]
        [MaxLength(30)]
        public string City { get; set; }

        [MaxLength(50)]
        public string Street { get; set; }

        [Range(0, 9999999)]
        public int? ZipCode { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string UserName => this.FirstName[0] + "." + this.LastName;
    }
}
