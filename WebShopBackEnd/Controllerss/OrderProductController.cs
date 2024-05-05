using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopBackEnd.Interfaces;
using WebShopBackEnd.Models;
using WebShopBackEnd.Repositories;

namespace WebShopBackEnd.Controllerss
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderProductRepository _opRepository;
        private readonly IMapper _mapper;

        public OrderProductController(IOrderProductRepository opRepository, IMapper mapper)
        {
            _opRepository = opRepository;
            _mapper = mapper;
        }

        [HttpGet]

        public ActionResult<IEnumerable<OrderProduct>> getOrderProducts()
        {

            return Ok(_opRepository.GetOrderProducts());
        }


        [HttpPost]
        public ActionResult<OrderProduct> createOrderProduct(OrderProduct op)
        {

            _opRepository.CreateOrderProduct(op);
            _opRepository.saveChanges();


            return op;

        }

        [HttpPut]
        public ActionResult<OrderProduct> updateOrderProduct(OrderProduct op)
        {
            var oldOP = _opRepository.GetOrderProducts().FirstOrDefault(o=> o.ProductId==op.ProductId && o.OrderId==op.OrderId);
            if (oldOP == null)
            {
                return NotFound();
            }
            OrderProduct opEntity = _mapper.Map<OrderProduct>(op);
            _mapper.Map(opEntity, oldOP);
            _opRepository.saveChanges();

            return op;

        }

        [HttpDelete("ByOrderId/{OrderId}")]
        [Authorize(Roles = "admin")]
        public ActionResult deleteOrderProductByOrderId(int OrderId)
        {

            var orders = _opRepository.GetOrderProducts().Where(op=> op.OrderId==OrderId).ToList();
            if (orders == null)
            {
                return NotFound();
            }
            _opRepository.deleteOrderProductByOrderId(OrderId);
            _opRepository.saveChanges();
            return NoContent();


        }
        [HttpDelete("ByProductId/{ProductId}")]
        [Authorize(Roles = "admin")]
        public ActionResult deleteOrderProductByProductId(int ProductId)
        {

            var orders = _opRepository.GetOrderProducts().Where(op => op.ProductId == ProductId).ToList();
            Console.WriteLine(orders);
            if (orders == null)
            {
                return NotFound();
            }
            _opRepository.deleteOrderProductByProductId(ProductId);
            _opRepository.saveChanges();
            return NoContent();


        }
    }
}
