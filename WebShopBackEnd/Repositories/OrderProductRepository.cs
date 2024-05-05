using WebShopBackEnd.Interfaces;
using WebShopBackEnd.Models;

namespace WebShopBackEnd.Repositories
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly AppDbContext _context;
        public OrderProductRepository(AppDbContext context)
        {
            _context = context;
        }


        
        public void CreateOrderProduct(OrderProduct orderProduct)
        {
            if (orderProduct == null)
            {
                throw new ArgumentNullException(nameof(orderProduct));
            }
            _context.OrderProducts.Add(orderProduct);
        }

        public void deleteOrderProductByOrderId( int orderId)
        {
            var orderProduct = _context.OrderProducts.Where(o => o.OrderId == orderId).ToList();
            if (orderProduct == null)
            {
                throw new ArgumentNullException(nameof(orderProduct));
            }

            _context.OrderProducts.RemoveRange(orderProduct);
        }
        public void deleteOrderProductByProductId(int ProductId)
        {
            var orderProducts = _context.OrderProducts.Where(o => o.ProductId == ProductId).ToList();
            if (orderProducts == null)
            {
                throw new ArgumentNullException(nameof(orderProducts));
            }

            _context.OrderProducts.RemoveRange(orderProducts);
        }

        public ICollection<OrderProduct> GetOrderProducts()
        {
           return _context.OrderProducts.ToList();
        }

        public bool saveChanges()
        {
           return (_context.SaveChanges()<=0);
        }

        public void UpdateOrderProduct(OrderProduct orderProduct)
        {
            throw new NotImplementedException();
        }
    }
}
