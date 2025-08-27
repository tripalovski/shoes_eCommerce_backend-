using eCommerce_backend.Constants;

namespace eCommerce_backend.DTOs
{
    public record OrderItemDto(int FootwearId, int Quantity);

    public record OrderDto(ICollection<OrderItemDto> Items);

    public record OrderItemDisplayDto(
        int FootwearId,
        string Name,
        decimal Price,
        int Quantity
    );

    public record OrderDisplayDto(
        int Id, 
        DateTime OrderDate, 
        OrderStatus OrderStatus,

        ICollection<OrderItemDisplayDto> Items
    );
}
