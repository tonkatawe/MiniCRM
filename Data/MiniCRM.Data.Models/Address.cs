namespace MiniCRM.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MiniCRM.Data.Common.Models;

    public class Address : BaseModel<int>
    {
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
