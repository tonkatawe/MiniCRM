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
            this.Sales = new HashSet<Sale>();
        }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName => string.IsNullOrEmpty(this.MiddleName)
            ? (this.FirstName + " " + this.LastName)
            : (this.FirstName + " " + this.MiddleName.Substring(0, 1) + ". " + this.LastName);

        public int JobTitleId { get; set; }

        public virtual JobTitle JobTitle { get; set; }

        public int AddressId { get; set; }

        [Required]
        public virtual Address Address { get; set; }

        [Required]
        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public string AccountId { get; set; }

        public string OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }

        public bool HasAccount { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public virtual IEnumerable<Customer> Customers { get; set; }

        public virtual IEnumerable<Sale> Sales { get; set; }
    }
}
