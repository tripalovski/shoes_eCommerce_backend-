using eCommerce_backend.Constants;

namespace eCommerce_backend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAtDate { get; set; }
        public OrderStatus Status { get; set; }

        // navigation property for related OrderItems
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
