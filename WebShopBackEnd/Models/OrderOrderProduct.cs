namespace WebShopBackEnd.Models
{
    public class OrderOrderProduct
    {
        public ShopOrder order { get; set; }
        
        public List<ProductQuantity> products { get; set; }
    }
}
