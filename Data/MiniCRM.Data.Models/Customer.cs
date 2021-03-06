﻿using System.Collections.Generic;

namespace MiniCRM.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MiniCRM.Data.Common.Models;

    public class Customer : BaseDeletableModel<int>
    {
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

        public string OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }

        public int EmployerId { get; set; }

        public Employer Employer { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public virtual ICollection<Sale> Orders { get; set; }
    }
}
