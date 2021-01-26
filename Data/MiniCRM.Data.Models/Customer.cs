namespace MiniCRM.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MiniCRM.Common;
    using MiniCRM.Data.Common.Models;

    public class Customer : BaseDeletableModel<int>
    {
        public Customer()
        {
            this.Phones = new HashSet<PhoneNumber>();
            this.Emails = new HashSet<EmailAddress>();
            this.Orders = new HashSet<Order>();
        }

        [Required]
        [MaxLength(GlobalConstants.MaxNamesLength)]
        public string FirstName { get; set; }

        [MaxLength(GlobalConstants.MaxNamesLength)]

        public string MiddleName { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxNamesLength)]
        public string LastName { get; set; }

        public string FullName => string.IsNullOrEmpty(this.MiddleName)
            ? (this.FirstName + " " + this.LastName)
            : (this.FirstName + " " + this.MiddleName.Substring(0, 1) + ". " + this.LastName);

        public int EmployerId { get; set; }

        public virtual Employer Employer { get; set; }

        public string JobTitle { get; set; }

        public bool IsTemporary { get; set; }

        public bool HasAccount { get; set; }

        [MaxLength(1000)]
        public string AdditionalInfo { get; set; }


        public virtual ICollection<PhoneNumber> Phones { get; set; }

        public virtual ICollection<EmailAddress> Emails { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
