using eCommerce_backend.Constants;
using eCommerce_backend.Database;
using eCommerce_backend.DTOs;
using eCommerce_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

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

        [Authorize(Roles = nameof(Role.User))]
        [HttpPost("createOrder")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderDto orderDto) {
            if (orderDto == null || orderDto.Items.Count == 0) {
                return BadRequest("Order cannot be empty.");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) {
                return Unauthorized("User ID not found in token.");
            }

            var newOrder = new Order {
                CreatedAtDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                UserId = int.Parse(userId),
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

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpGet("getAllOrders")]
        public async Task<ActionResult<IEnumerable<OrderDisplayDto>>> GetOrders() {
            var orders = await _context.Order
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Footwear)
                .Select(o => new OrderDisplayDto(
                    o.Id,
                    o.CreatedAtDate,
                    o.Status,
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

        [Authorize(Roles = nameof(Role.User))]
        [HttpGet("getUserOrders")]
        public async Task<ActionResult<IEnumerable<OrderDisplayDto>>> GetUserOrders() {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) {
                return Unauthorized("User ID not found in token.");
            }
            var orders = await _context.Order
                .Where(o => o.UserId == int.Parse(userId))
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Footwear)
                .Select(o => new OrderDisplayDto(
                    o.Id,
                    o.CreatedAtDate,
                    o.Status,
                    o.OrderItems.Select(oi => new OrderItemDisplayDto(
                        oi.FootwearId,
                        oi.Footwear!.Name,
                        oi.Footwear!.Price,
                        oi.Quantity
                    )).ToList()
                ))
                .ToListAsync();
            if (orders == null || !orders.Any()) {
                return NotFound("No orders found for this user.");
            }
            return Ok(orders);
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpPatch("updateStatus/{orderId}")]
        public async Task<IActionResult> MarkOrderCompleted(int orderId, OrderStatusUpdateDto statusUpdateDto) {
            var order = await _context.Order.FindAsync(orderId);
            if (order == null) {
                return NotFound("Order not found.");
            }

            order.Status = statusUpdateDto.Status;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpDelete("deleteOrder/{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId) {
            var order = await _context.Order
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null) {
                return NotFound("Order not found.");
            }
            // First remove associated order items
            _context.OrderItem.RemoveRange(order.OrderItems);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpDelete("deleteItem/{orderId}/{footwearId}")]
        public async Task<IActionResult> RemoveItemFromOrder(int orderId, int footwearId) {
            var order = await _context.Order
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            var orderItem = order?.OrderItems.FirstOrDefault(oi => oi.FootwearId == footwearId);

            if(orderItem == null) {
                return NotFound("Order item not found.");
            }

            _context.OrderItem.Remove(orderItem);
            if (order?.OrderItems.Count == 1) {
                // If it was the last item, remove the order as well
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
