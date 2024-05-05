using WebShopBackEnd.Interfaces;
using WebShopBackEnd.Models;

namespace WebShopBackEnd.Repositories
{
    public class ShopOrderRepository : IShopOrderRepository
    {

        private readonly AppDbContext _context;
        public ShopOrderRepository(AppDbContext context)
        {
            _context = context;
        }


      
        public void CreateOrder(ShopOrder order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            _context.ShopOrders.Add(order);
        }

        public void DeleteOrder(int OrderId)
        {
            var order = _context.ShopOrders.FirstOrDefault(o => o.OrderId == OrderId);
            if (order != null)
            {
                _context.ShopOrders.Remove(order);
            }
            else throw new ArgumentNullException(nameof(order));
        }

        public ICollection<ShopOrder> GetOrders()
        {
            return _context.ShopOrders.ToList();
        }

        public ShopOrder GetOrderByID(int OrderId)
        {
            var order = _context.ShopOrders.FirstOrDefault(o => o.OrderId == OrderId);
            if (order != null)
            {
                return order;
            }
            else throw new ArgumentNullException(nameof(order));
        }

        public bool saveChanges()
        {
           return (_context.SaveChanges() <= 0);
        }

        public void UpdateOrder(ShopOrder order)
        {
            throw new NotImplementedException();
        }
    }
}
