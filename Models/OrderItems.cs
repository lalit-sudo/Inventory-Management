namespace InventoryManagement.Models
{
    public class OrderItems
    {
        public int ProductId { get;private set; }
        public int Quantity { get;private set; }
        public decimal Price{ get;private set; }

        public OrderItems(int productId, int qty, decimal price)
        {
            this.ProductId = productId;
            this.Quantity = qty;
            this.Price = price;
        }
    }
}
