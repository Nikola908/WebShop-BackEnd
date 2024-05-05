using WebShopBackEnd.Models;

namespace WebShopBackEnd.Interfaces
{
    public interface IShopOrderRepository
    {
        ICollection<ShopOrder> GetOrders();

        ShopOrder GetOrderByID(int OrderId);
        void CreateOrder(ShopOrder order);
        void UpdateOrder(ShopOrder order);
        void DeleteOrder(int OrderId);
        bool saveChanges();
    }
}
