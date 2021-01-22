namespace MiniCRM.Web.ViewModels.Companies
{
    using System.ComponentModel.DataAnnotations;

    public class CompanyCreateModel
    {
        public string UserId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Company name should be minimum 3 letters")]
        [MaxLength(50, ErrorMessage = "Company name should be maximum 50 letters")]
        public string Name { get; set; }

        [MinLength(50, ErrorMessage = "Company description should be minimum 50 letters")]
        [MaxLength(2500, ErrorMessage = "Company description should be maximum 2500 letters")]
        public string Description { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Industry type should be minimum 3 letters")]

        [MaxLength(150, ErrorMessage = "Industry type should be maximum 150 letters")]
        public string IndustryName { get; set; }

        public bool IsPublic { get; set; }

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
    }
}
