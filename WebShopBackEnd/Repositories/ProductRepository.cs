using WebShopBackEnd.Interfaces;
using WebShopBackEnd.Models;

namespace WebShopBackEnd.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }


        public void CreateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _context.Products.Add(product);
        }

        public void DeleteProduct(int ProductId)
        {
            var product = _context.Products.FirstOrDefault(u => u.ProductId == ProductId);
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Products.Remove(product);
        }

        public Product GetProductByID(int ProductId)
        {
            var product = _context.Products.FirstOrDefault(u => u.ProductId == ProductId);
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            return product;
        }

        public ICollection<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public bool saveChanges()
        {
            return (_context.SaveChanges() <= 0);
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
