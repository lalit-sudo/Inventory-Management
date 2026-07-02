using InventoryManagement.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SalesSummaryController : ControllerBase
    {
        private OrderService _orderService;
        public SalesSummaryController(OrderService orderService)
        {
            _orderService = orderService;  
        }

        [HttpGet]
        public IActionResult GetSalesSummary(DateOnly startDate, DateOnly endDate)
        {
            var salesSummary = _orderService.GetSalesSummary(startDate, endDate);
            return Ok(salesSummary);
        }

    }
}
