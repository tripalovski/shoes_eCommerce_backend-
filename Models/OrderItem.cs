namespace eCommerce_backend.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int FootwearId { get; set; }
        public int Quantity { get; set; }

        // navigation properties
        public Order? Order { get; set; }
        public Footwear? Footwear { get; set; } // Pretpostavka da postoji Footwear model
    }
}