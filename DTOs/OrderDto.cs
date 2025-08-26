namespace eCommerce_backend.DTOs
{
    public record OrderItemDto(int FootwearId, int Quantity);

    public record OrderDto(ICollection<OrderItemDto> Items);
}
