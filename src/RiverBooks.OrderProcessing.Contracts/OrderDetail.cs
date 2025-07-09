namespace RiverBooks.OrderProcessing.Contracts;

public class OrderDetail
{
    public required DateTimeOffset DateCreated { get; set; }
    public required Guid OrderId { get; set; }
    public required Guid UserId { get; set; }
    public required List<OrderItemDetail> OrderItems { get; set; }
}
