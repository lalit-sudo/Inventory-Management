using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.DTO
{
    public class OrderItemsDto
    {
        [Required]
        public int ProductId{ get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
