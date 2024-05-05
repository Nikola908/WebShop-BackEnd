namespace WebShopBackEnd.Models
{
    public class CheckOutProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string? ProductImage { get; set; }
    }
}
