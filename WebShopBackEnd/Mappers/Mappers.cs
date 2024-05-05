using AutoMapper;
using WebShopBackEnd.Models;

namespace WebShopBackEnd.Mappers
{
    public class Mappers : Profile
    {

        public Mappers() {
            CreateMap<ShopUser, ShopUser>();
            CreateMap<Category, Category>();
            CreateMap<Product, Product>();
            CreateMap<Review, Review>();
            CreateMap<OrderProduct, OrderProduct>();
            CreateMap<ShopOrder, ShopOrder>();

        }
        
    }
}
