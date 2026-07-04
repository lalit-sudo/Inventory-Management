using Inventory_Management.Services;
using InventoryManagement.DTO;
using InventoryManagement.DTO.Order;
using InventoryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace InventoryManagement.Services
{
    public class OrderService
    {
        private InventoryManagementContext _context;
        private InventoryService _inventoryService;
        public OrderService(InventoryManagementContext context, InventoryService inventoryService)
        {
            _context = context;
            _inventoryService = inventoryService;
        }


        public string PlaceOrder(List<OrderItemsDto> items_list)
        {
            List<OrderItem> availableItems = new List<OrderItem>();

            //Check item availability and fetch price of available items and discard unavailable ones
            foreach (OrderItemsDto item in items_list)
            {
                decimal itemPrice = _inventoryService.CheckStockAvailablity(item.ProductId, item.Quantity);
                if (itemPrice != -1)
                {
                    OrderItem newOrderItem = new OrderItem();
                    newOrderItem.ProductId = item.ProductId;
                    newOrderItem.Quantity = item.Quantity;
                    newOrderItem.Price = itemPrice;
                    availableItems.Add(newOrderItem);
                }
            }

            if (availableItems.Count > 0)
            {
                // Bill calculation for whole order
                decimal totalAmount = availableItems.Sum(p => (p.Price * p.Quantity));
                try
                {
                    //Update stock
                    foreach (var item in availableItems)
                    {
                        _inventoryService.ReduceQty(item.ProductId, item.Quantity);
                    }

                    //Add order to Orders Table
                    Order order = new Order();
                    order.TotalAmount = totalAmount;
                    order.OrderItems = availableItems;
                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    return $"Order placed with order id: {order.OrderId}.";
                }
                catch (Exception ex)
                {
                    return $"Error occured while placing order:- {ex.ToString()}";
                }
                
            }
            else
            {
                return "No valid items for ordering";
            }
        }
        
        public List<OrderDto> GetOrders()
        {
            List<OrderDto> orders = new List<OrderDto>();
            orders = _context.OrderItems.Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                OrderDate = o.Order.OrderDate,
                TotalAmount = o.Order.TotalAmount,
                Quantity = o.Quantity,
                Price = o.Price,
                ProductName = o.Product.ProductName
            }).ToList();
            return orders;
        }

        public SalesSummaryDto GetSalesSummary(DateOnly startDate, DateOnly endDate)
        {
            DateTime startDT = startDate.ToDateTime(TimeOnly.MinValue);
            DateTime endDT = endDate.ToDateTime(TimeOnly.MaxValue);

            //Filter orders for specifed time window
            var filteredOrders = _context.Orders.Where(o => o.OrderDate>= startDT && o.OrderDate<= endDT);

            SalesSummaryDto salesSummary = new SalesSummaryDto();
            
            //Count total orders
            salesSummary.TotalOrders = filteredOrders.Count();
            //Total Revenue
            salesSummary.TotalRevenue = filteredOrders.Sum(o => o.TotalAmount);
            //No. of Products Sold
            salesSummary.ProductsSold = filteredOrders.SelectMany(o => o.OrderItems).Sum(i => i.Quantity);

            return salesSummary;
        }

    }
}
