namespace MiniCRM.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MiniCRM.Data.Common.Models;

    public class Company : BaseDeletableModel<string>, IAuditInfo
    {
        public Company()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Products = new HashSet<Product>();
            this.UserCompanies = new HashSet<UserCompanies>();
            this.Users = new HashSet<ApplicationUser>();
        }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [MaxLength(2500)]
        public string Description { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public int IndustryId { get; set; }

        public Industry Industry { get; set; }

        public bool IsPublic { get; set; }

        public virtual ICollection<UserCompanies> UserCompanies { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
