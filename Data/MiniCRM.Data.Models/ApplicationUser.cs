// ReSharper disable VirtualMemberCallInConstructor

namespace MiniCRM.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;
    using MiniCRM.Common;
    using MiniCRM.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Orders = new HashSet<Order>();
            this.Customers = new HashSet<Customer>();
            this.Employers = new HashSet<Employer>();
            this.UserCompanies = new HashSet<UserCompanies>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

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

        public string ProfilePictureUrl { get; set; }

        public string CompanyId { get; set; }

        public int? JobTitleId { get; set; }

        public virtual JobTitle JobTitle { get; set; }

        public virtual Company Company { get; set; }

        public virtual ApplicationUser Parent { get; set; }

        public string? ParentId { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        public virtual ICollection<Employer> Employers { get; set; }

        public virtual ICollection<UserCompanies> UserCompanies { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
