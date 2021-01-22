namespace MiniCRM.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MiniCRM.Data.Common.Models;

    public class Order : BaseDeletableModel<int>
    {
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
