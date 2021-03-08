namespace MiniCRM.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MiniCRM.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.Sales = new HashSet<SaleProduct>();
            //  this.Customers = new HashSet<ApplicationUser>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<SaleProduct> Sales { get; set; }

        public string ProductPictureUrl { get; set; }
    }
}
