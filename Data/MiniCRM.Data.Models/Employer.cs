namespace MiniCRM.Data.Models
{
    using System.Collections.Generic;

    using MiniCRM.Data.Common.Models;

    public class Employer : BaseDeletableModel<int>
    {
        public Employer()
        {
            this.Phones = new HashSet<PhoneNumber>();
            this.Emails = new HashSet<EmailAddress>();
            this.Customers = new HashSet<Customer>();
            this.Products = new HashSet<Product>();
        }
        public string CompanyId { get; set; }

        public Company Company { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName => string.IsNullOrEmpty(this.MiddleName)
            ? (this.FirstName + " " + this.LastName)
            : (this.FirstName + " " + this.MiddleName.Substring(0, 1) + ". " + this.LastName);

        public string JobTitle { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<PhoneNumber> Phones { get; set; }

        public virtual ICollection<EmailAddress> Emails { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
