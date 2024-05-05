using System;
using System.Collections.Generic;

namespace WebShopBackEnd.Models
{
    public partial class ShopUser
    {
        public ShopUser()
        {
            Reviews = new HashSet<Review>();
            ShopOrders = new HashSet<ShopOrder>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string? UserRole { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<ShopOrder> ShopOrders { get; set; }
    }
}
