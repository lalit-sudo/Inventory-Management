using Inventory_Management.Models;
using Inventory_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        
        private InventoryService _service;
        public ProductController(InventoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _service.DisplayProducts();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            string result = _service.AddProduct(product);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult RemoveProduct(int productId)
        {
            string? result = _service.RemoveProduct(productId);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult FetchProductByPrice(decimal price)
        {
            return Ok(_service.FetchProductsByPrice(price));
        }

        [HttpGet]
        public IActionResult FindByProductId(int productId)
        {
            Product? product = _service.FindByProductId(productId);
            return Ok(product);
        }

        [HttpGet]
        public IActionResult SortByPrice(string sorting_order = "asc")
        {
            List<Product> sorted_Products = _service.SortByPrice(sorting_order);
            return Ok(sorted_Products);
        }

        [HttpGet]
        public IActionResult GetProductNames()
        {
            List<string> productNames = _service.DisplayProductNames();
            return Ok(productNames);
        }

    }
}
