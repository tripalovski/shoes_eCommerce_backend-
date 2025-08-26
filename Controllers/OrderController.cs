using eCommerce_backend.Database;
using eCommerce_backend.DTOs;
using eCommerce_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context) {
            _context = context;
        }

        [HttpPost("createOrder")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderDto orderDto) {
            if (orderDto == null || orderDto.Items.Count == 0) {
                return BadRequest("Order cannot be empty.");
            }

            var newOrder = new Order {
                OrderDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>()
            };

            foreach (var itemDto in orderDto.Items) {
                newOrder.OrderItems.Add(new OrderItem {
                    FootwearId = itemDto.FootwearId,
                    Quantity = itemDto.Quantity
                });
            }

            _context.Order.Add(newOrder);
            await _context.SaveChangesAsync();

            return Ok(new { orderId = newOrder.Id });
        }

        //[HttpGet("{orderId}")]

        [HttpGet("getAllOrders")]
        public async Task<ActionResult<IEnumerable<OrderDisplayDto>>> GetOrders() {
            var orders = await _context.Order
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Footwear)
                .Select(o => new OrderDisplayDto(
                    o.Id,
                    o.OrderDate,
                    o.OrderItems.Select(oi => new OrderItemDisplayDto(
                        oi.FootwearId,
                        oi.Footwear!.Name,
                        oi.Footwear!.Price,
                        oi.Quantity
                    )).ToList()
                ))
                .ToListAsync();

            if (orders == null || !orders.Any()) {
                return NotFound("No orders found.");
            }

            return Ok(orders);
        }
    }
}
