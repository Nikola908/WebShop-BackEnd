using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopBackEnd.Interfaces;
using WebShopBackEnd.Models;
using WebShopBackEnd.Repositories;

namespace WebShopBackEnd.Controllerss
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopOrderController : ControllerBase
    {

        private readonly IShopOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IOrderProductRepository _orderProductRepository;

        public ShopOrderController(IShopOrderRepository orderRepository, IMapper mapper,IOrderProductRepository orderProductRepository)
        {
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
            _mapper = mapper;
        }

        [HttpGet]

        public ActionResult<IEnumerable<ShopOrder>> getOrders()
        {

            return Ok(_orderRepository.GetOrders());
        }

        [HttpGet("{OrderId}")]
        public ActionResult<ShopOrder > getOrderById(int OrderId)
        {
            var order = _orderRepository.GetOrderByID(OrderId);

            if (order != null)
            {
                return Ok(_orderRepository.GetOrderByID(OrderId));
            }
            else return NotFound();
        }
        [HttpPost]
        public ActionResult<ShopOrder> createOrder(ShopOrder order)
        {

            _orderRepository.CreateOrder(order);
            _orderRepository.saveChanges();
            return order;


        }
        [HttpPost("/ProductOrder")]
        public void createOrderProduct(OrderOrderProduct orderProduct)
        {

            _orderRepository.CreateOrder(orderProduct.order);

            _orderRepository.saveChanges();
             var orderId = _orderRepository.GetOrders().Last().OrderId;
            foreach ( var product in orderProduct.products)
            {
                _orderProductRepository.CreateOrderProduct(new OrderProduct { OrderId=orderId,ProductId=product.ProductId,Quantity=product.quantity});
            }
            _orderProductRepository.saveChanges();

        }

        [HttpPut]
        public ActionResult<ShopOrder> updateOrder(ShopOrder order)
        {
            var oldOrder = _orderRepository.GetOrderByID(order.OrderId);
            if (oldOrder == null)
            {
                return NotFound();
            }
            ShopOrder newOrder = _mapper.Map<ShopOrder>(order);
            _mapper.Map(newOrder, oldOrder);
            _orderRepository.saveChanges();


            return order;

        }

        [HttpDelete("{orderId}")]
        public ActionResult<ShopOrder> deleteOrder(int OrderId)
        {
            var order = _orderRepository.GetOrderByID(OrderId);
            if (order == null)
            {
                return NotFound();
            }

            _orderRepository.DeleteOrder(OrderId);
            _orderRepository.saveChanges();
            return order;
        }
    }
}
