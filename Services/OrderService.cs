using Inventory_Management.Services;
using InventoryManagement.DTO;
using InventoryManagement.Models;
using System.Runtime.CompilerServices;

namespace InventoryManagement.Services
{
    public class OrderService
    {
        private int _nextOrderId = 1;
        private List<Order> _orders;
        private InventoryService _inventoryService;
        public OrderService(InventoryService inventoryService)
        {
            _orders = new List<Order>();
            _inventoryService = inventoryService;
        }

        public string PlaceOrder(List<OrderItemsDto> items_list)
        {
            List<OrderItems> availableItems = new List<OrderItems>();

            //Check item availability and fetch price of available items and discard unavailable ones
            foreach (OrderItemsDto item in items_list)
            {
                decimal itemPrice = _inventoryService.CheckStockAvailablity(item.ProductId, item.Quantity);
                if (itemPrice != -1)
                {
                    availableItems.Add(new OrderItems(item.ProductId, item.Quantity, itemPrice));
                }
            }

            if (availableItems.Count > 0)
            {
                // Bill calculation for whole ord
                decimal totalAmount = availableItems.Sum(p => (p.Price * p.Quantity));
                
                //Update stock
                foreach (var item in items_list)
                {
                    _inventoryService.ReduceQty(item.ProductId, item.Quantity);
                }
                
                //Add order to Orders List
                Order order = new Order(_nextOrderId++, totalAmount, availableItems);
                _orders.Add(order);
                return $"Order placed with order id: {order.OrderId} and contains total {order.Items.Count} items.";
            }
            else
            {
                return "No valid items for ordering";
            }
        }
        
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            foreach(var order in _orders)
            {
                orders.Add(order);
            }
            return orders;
        }

        public SalesSummaryDto GetSalesSummary(DateOnly startDate, DateOnly endDate)
        {
            DateTime startDT = startDate.ToDateTime(TimeOnly.MinValue);
            DateTime endDT = endDate.ToDateTime(TimeOnly.MaxValue);

            //Filter orders for specifed time window
            var filteredOrders = _orders.Where(o => o.OrderDate>= startDT && o.OrderDate<= endDT);

            SalesSummaryDto salesSummary = new SalesSummaryDto();
            
            //Count total orders
            salesSummary.TotalOrders = filteredOrders.Count();
            //Total Revenue
            salesSummary.TotalRevenue = filteredOrders.Sum(o => o.TotalAmount);
            //No. of Products Sold
            salesSummary.ProductsSold = filteredOrders.SelectMany(o => o.Items).Sum(i => i.Quantity);

            return salesSummary;
        }

    }
}
