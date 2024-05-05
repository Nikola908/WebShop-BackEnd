using WebShopBackEnd.Interfaces;
using WebShopBackEnd.Models;

namespace WebShopBackEnd.Repositories
{
    public class ShopUserRepository : IShopUser
    {
        private readonly AppDbContext _context;
        public ShopUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateUser(ShopUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.ShopUsers.Add(user);
        }

        public void DeleteUser(int UserId)
        {
            var user = _context.ShopUsers.FirstOrDefault(u => u.UserId == UserId);
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }

            _context.ShopUsers.Remove(user);
        }

        public ShopUser GetUserByID(int userID)
        {
            var user = _context.ShopUsers.FirstOrDefault(u => u.UserId == userID);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return user;
        }

        public ICollection<ShopUser> GetUsers()
        {
           return _context.ShopUsers.ToList();
        }

        public bool saveChanges()
        {
                return (_context.SaveChanges() <= 0);
        }

        public void UpdateUser(ShopUser user)
        {
            throw new NotImplementedException();
        }
    }
}
