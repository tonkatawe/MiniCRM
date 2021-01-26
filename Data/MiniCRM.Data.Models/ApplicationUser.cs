// ReSharper disable VirtualMemberCallInConstructor

using MiniCRM.Common;

namespace MiniCRM.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;
    using MiniCRM.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Employees = new HashSet<Employer>();
            this.Orders = new HashSet<Order>();
            this.PhoneNumbers = new HashSet<PhoneNumber>();
            this.EmailAddresses = new HashSet<EmailAddress>();
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

        public virtual Company Company { get; set; }

        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ApplicationUser Parent { get; set; }

        public string? ParentId { get; set; }

        public virtual ICollection<Employer> Employees { get; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }

        public virtual ICollection<EmailAddress> EmailAddresses { get; set; }

        public virtual ICollection<UserCompanies> UserCompanies { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
