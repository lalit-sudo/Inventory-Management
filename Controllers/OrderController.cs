using InventoryManagement.DTO;
using Inventory_Management.Services;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management.Models;
using InventoryManagement.Models;

namespace InventoryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private OrderService _orderService;
        private InventoryService _inventoryService;
        public OrderController(OrderService orderService, InventoryService inventoryService)
        {
            _orderService = orderService;
            _inventoryService = inventoryService;
        }

        [HttpPost]
        public IActionResult PlaceOrder(List<OrderItemsDto> items_list)
        {
            string result = _orderService.PlaceOrder(items_list);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _orderService.GetOrders();
            return Ok(orders);
        }

    }
}
