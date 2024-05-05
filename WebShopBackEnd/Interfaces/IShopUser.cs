using WebShopBackEnd.Models;

namespace WebShopBackEnd.Interfaces
{
    public interface IShopUser
    {
        ICollection<ShopUser> GetUsers();

        ShopUser GetUserByID(int userID);
        void CreateUser(ShopUser user);
        void UpdateUser(ShopUser user);
        void DeleteUser(int UserId);
        bool saveChanges();
    }
}
