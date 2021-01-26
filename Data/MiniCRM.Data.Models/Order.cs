namespace MiniCRM.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MiniCRM.Data.Common.Models;

    public class Order : BaseDeletableModel<int>
    {
        [Required]
        public int EmployerId { get; set; }

        public virtual Employer Employer { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
