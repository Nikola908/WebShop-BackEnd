using System;
using System.Collections.Generic;

namespace WebShopBackEnd.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public string? ReviewDescription { get; set; }
        public double? Rating { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual ShopUser User { get; set; } = null!;
    }
}
