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
    public class CategoryController : ControllerBase
    {
        private readonly IcategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(IcategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<Category>> getCategories()
        {

            return Ok(_categoryRepository.GetCategories());
        }

        [HttpGet("{CategoryId}", Name = "getCategoryByID")]
        [Authorize(Roles = "admin")]
        public ActionResult<Category> getCategoryByID(int CategoryId)
        {
            var category = _categoryRepository.GetCategoryByID(CategoryId);

            if (category != null)
            {
                return Ok(_categoryRepository.GetCategoryByID(CategoryId));
            }
            else return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<Category> createCategory(Category category)
        {

            _categoryRepository.CreateCategory(category);
            _categoryRepository.saveChanges();


            return category;

        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public ActionResult<Category> updateCategory(Category category)
        {
            var oldCategory = _categoryRepository.GetCategoryByID(category.CategoryId);
            if (oldCategory == null)
            {
                return NotFound();
            }
            Category categoryEntity = _mapper.Map<Category>(category);
            _mapper.Map(categoryEntity, oldCategory);
            _categoryRepository.saveChanges();

            return category;

        }

        [HttpDelete("{CategoryId}")]
        [Authorize(Roles = "admin")]
        public ActionResult deleteCategory(int CategoryId)
        {

            var category = _categoryRepository.GetCategoryByID(CategoryId);
            if (category == null)
            {
                return NotFound();
            }
            _categoryRepository.DeleteCategory(category.CategoryId);
            _categoryRepository.saveChanges();
            return NoContent();


        }
    }
}
