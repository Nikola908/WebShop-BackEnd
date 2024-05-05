using WebShopBackEnd.Models;

namespace WebShopBackEnd.Interfaces
{
    public interface IcategoryRepository
    {

        ICollection<Category> GetCategories();

        Category GetCategoryByID(int CategoryId);
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int CategoryId);
        bool saveChanges();
    }
}
