using Ardalis.Result;
using MediatR;

namespace RiverBooks.OrderProcessing.Contracts;

public record CreateOrderCommand(
    Guid UserId,
    Guid ShippingAddressId,
    Guid BillingAddressId,
    List<OrderItemDetail> OrderItems)
    : IRequest<Result<OrderDetailsResponse>>;
