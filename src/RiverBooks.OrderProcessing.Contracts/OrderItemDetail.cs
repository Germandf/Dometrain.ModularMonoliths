namespace RiverBooks.OrderProcessing.Contracts;

public record OrderItemDetail(
    Guid BookId,
    int Quantity,
    decimal UnitPrice,
    string Description);
