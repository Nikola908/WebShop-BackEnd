using System;
using System.Collections.Generic;

namespace WebShopBackEnd.Models
{
    public partial class ShopOrder
    {
        public ShopOrder()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? OrderStatus { get; set; }
        public int UserId { get; set; }

        public virtual ShopUser? User { get; set; } = null!;
        public virtual ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
