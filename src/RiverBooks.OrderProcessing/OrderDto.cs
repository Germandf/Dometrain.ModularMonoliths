namespace RiverBooks.OrderProcessing;

public record OrderDto(
    Guid Id,
    Guid UserId,
    DateTimeOffset DateCreated,
    DateTimeOffset? DateShipped,
    decimal Total);
