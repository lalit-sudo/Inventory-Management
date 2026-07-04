using Inventory_Management.Models;

namespace InventoryManagement.Models
{
    public class OrderItem
    {
        public int OrderItemId {  get; set; }  //Primary Key
        public int ProductId { get; set; }  //Foreign Key
        public Product Product { get; set; } = null!;// Reference Navigation Property
        public int Quantity { get; set; }
        public decimal Price{ get; set; }

        public int OrderId { get; set; }  //Foreign Key
        public Order Order { get; set; } = null!; //Reference Navigation Property

        
    }
}
