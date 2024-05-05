using WebShopBackEnd.Models;

namespace WebShopBackEnd.Interfaces
{
    public interface IProductRepository
    {

        ICollection<Product> GetProducts();

        Product GetProductByID(int ProductId);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int ProductId);
        bool saveChanges();
    }
}
