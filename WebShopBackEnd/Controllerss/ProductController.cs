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
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<Product>> getProducts()
        {

            return Ok(_productRepository.GetProducts());
        }

        [HttpGet("{ProductId}", Name = "getProductByID")]
        [AllowAnonymous]
        public ActionResult<Product> getProductByID(int ProductId)
        {
            var product = _productRepository.GetProductByID(ProductId);

            if (product != null)
            {
                return Ok(_productRepository.GetProductByID(ProductId));
            }
            else return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<Product> createProduct(Product product)
        {

            _productRepository.CreateProduct(product);
            _productRepository.saveChanges();


            return product;

        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public ActionResult<Product> updateProduct(Product product)
        {
            var oldProduct = _productRepository.GetProductByID(product.ProductId);
            if (oldProduct == null)
            {
                return NotFound();
            }
            Product productEntity = _mapper.Map<Product>(product);
            _mapper.Map(productEntity, oldProduct);
            _productRepository.saveChanges();

            return product;

        }

        [HttpDelete("{ProductId}")]
        [Authorize(Roles = "admin")]
        public ActionResult deleteProduct(int ProductId)
        {

            var product = _productRepository.GetProductByID(ProductId);
            if (product == null)
            {
                return NotFound();
            }
            _productRepository.DeleteProduct(product.ProductId);
            _productRepository.saveChanges();
            return NoContent();


        }
    }
}
