namespace InventoryManagement.Models
{
    public class Order
    {
        public int OrderId { get;  set; }
        public DateTime OrderDate { get;  set; } = DateTime.Now;
        public decimal TotalAmount { get;  set; }
        public ICollection<OrderItem> OrderItems { get;  set; } = new List<OrderItem>(); //Collection Navigation property

    }
}
