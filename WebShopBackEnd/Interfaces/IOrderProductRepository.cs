using WebShopBackEnd.Models;

namespace WebShopBackEnd.Interfaces
{
    public interface IOrderProductRepository
    {
        ICollection<OrderProduct> GetOrderProducts();

        void CreateOrderProduct(OrderProduct orderProduct);
        void UpdateOrderProduct(OrderProduct orderProduct);
        void deleteOrderProductByOrderId(int orderId);
        void deleteOrderProductByProductId(int productId);
        bool saveChanges();
    }
}
