namespace MiniCRM.Data.Models
{
    public class SaleProduct
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
