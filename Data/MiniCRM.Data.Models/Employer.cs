namespace MiniCRM.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MiniCRM.Data.Common.Models;

    public class Employer : BaseDeletableModel<int>
    {
        public Employer()
        {
            this.Customers = new HashSet<Customer>();
        }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName => string.IsNullOrEmpty(this.MiddleName)
            ? (this.FirstName + " " + this.LastName)
            : (this.FirstName + " " + this.MiddleName.Substring(0, 1) + ". " + this.LastName);

        public int? JobTitleId { get; set; }

        public virtual JobTitle JobTitle { get; set; }

        [MaxLength(1000)]
        public string AdditionalInfo { get; set; }

        public int AddressId { get; set; }

        [Required]
        public virtual Address Address { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public IEnumerable<Customer> Customers { get; set; }
    }
}
