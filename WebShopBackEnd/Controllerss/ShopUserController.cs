using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopBackEnd.Interfaces;
using WebShopBackEnd.Models;

namespace WebShopBackEnd.Controllerss
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopUserController : ControllerBase
    {
        private readonly IShopUser _shopUser;
        private readonly IMapper _mapper;

        public ShopUserController(IShopUser shopUser, IMapper mapper)
        {
            _shopUser = shopUser;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]

        public ActionResult<IEnumerable<ShopUser>> getUsers() {

            return Ok(_shopUser.GetUsers());
        }

        [HttpGet("{UserId}", Name = "getUserByID")]
        public ActionResult<ShopUser> getUserByID(int UserId)
        {
            var user = _shopUser.GetUserByID(UserId);

            if (user != null) {
                return Ok(_shopUser.GetUserByID(UserId));
            }
           else return NotFound();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<ShopUser> createUser(ShopUser user)
        {
           
             _shopUser.CreateUser(user);
            _shopUser.saveChanges();
            

            return user;
           
        }

        [HttpPut]
        [AllowAnonymous]
        public ActionResult<ShopUser> updateUser(ShopUser user)
        {
            var oldUser = _shopUser.GetUserByID(user.UserId);
            if (oldUser == null)
            {
                return NotFound();
            }
            ShopUser shopUserEntity = _mapper.Map<ShopUser>(user);
            _mapper.Map(shopUserEntity, oldUser);
            _shopUser.saveChanges(); 

            return user;

        }

        [HttpDelete("{UserId}")]
        [Authorize(Roles = "admin")]
        public ActionResult deleteUser(int UserId) {

            var user = _shopUser.GetUserByID(UserId);
            if (user == null)
            {
                return NotFound();
            }
                _shopUser.DeleteUser(user.UserId);
                _shopUser.saveChanges();
                return NoContent();
           

        }

    }
}
