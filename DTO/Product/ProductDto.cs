using Inventory_Management.Models;
using InventoryManagement.Models;

namespace InventoryManagement.DTO.Product
{
    public class ProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        
    }
}
