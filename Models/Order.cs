using eCommerce_backend.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce_backend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAtDate { get; set; }
        public OrderStatus Status { get; set; }
        public int UserId { get; set; }

        // navigation property for related OrderItems
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
