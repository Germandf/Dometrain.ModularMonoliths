using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Contracts;
using RiverBooks.OrderProcessing.Domain;
using RiverBooks.OrderProcessing.Interfaces;

namespace RiverBooks.OrderProcessing.Integrations;

internal class CreateOrderHandler(IOrderRepository orderRepository, IAddressCache addressCache) : IRequestHandler<CreateOrderCommand, Result<OrderDetailsResponse>>
{
    public async Task<Result<OrderDetailsResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var items = request.OrderItems
            .Select(item => new OrderItem(
                item.BookId,
                item.Description,
                item.Quantity,
                item.UnitPrice))
            .ToList();

        var shippingAddress = await addressCache.GetByIdAsync(request.ShippingAddressId);
        var billingAddress = await addressCache.GetByIdAsync(request.BillingAddressId);

        var order = Order.Factory.Create(
            request.UserId,
            shippingAddress,
            billingAddress,
            items);

        await orderRepository.AddAsync(order);
        await orderRepository.SaveChangesAsync();
        return new OrderDetailsResponse(order.Id);
    }
}
