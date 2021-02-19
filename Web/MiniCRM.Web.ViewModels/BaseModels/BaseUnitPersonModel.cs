namespace MiniCRM.Web.ViewModels.BaseModels
{
    using System.ComponentModel.DataAnnotations;

    public class BaseUnitPersonModel
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
        public string JobTitleName { get; set; }

        [Required]
        [MaxLength(30)]
        public string AddressCountry { get; set; }

        [Required]
        [MaxLength(30)]
        public string AddressCity { get; set; }

        [MaxLength(50)]
        public string AddressStreet { get; set; }

        [Range(0, 9999999)]
        public int? AddressZipCode { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
