using WebShopBackEnd.Interfaces;
using WebShopBackEnd.Models;

namespace WebShopBackEnd.Repositories
{
    public class CategoryRepository : IcategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

  
        public void CreateCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            _context.Categories.Add(category);
        }

        public void DeleteCategory(int CategoryId)
        {
            var category = _context.Categories.FirstOrDefault(u => u.CategoryId == CategoryId);
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _context.Categories.Remove(category);
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryByID(int CategoryId)
        {
            var category = _context.Categories.FirstOrDefault(u => u.CategoryId == CategoryId);
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            return category;
        }

        public bool saveChanges()
        {
            return (_context.SaveChanges() <= 0);
        }

        public void UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
