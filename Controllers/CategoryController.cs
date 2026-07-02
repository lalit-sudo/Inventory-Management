using Inventory_Management.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService; 
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            List<Category> categories = _categoryService.GetCategories();
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            string result = _categoryService.AddCategory(category);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult FindCategoryById(int categoryId)
        {
            Category? category = _categoryService.FindCategoryById(categoryId);
            return Ok(category);
        }

    }
}
