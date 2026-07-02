namespace InventoryManagement.Models
{
    public class Order
    {
        public int OrderId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public List<OrderItems> Items { get; private set; }

        public Order(int orderId, decimal totalAmount, List<OrderItems> items)
        {
            this.OrderId = orderId;
            this.OrderDate = DateTime.Now;
            this.TotalAmount = totalAmount;
            this.Items = items;
        }
    }
}
