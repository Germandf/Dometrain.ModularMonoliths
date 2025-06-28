using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Contracts;

namespace RiverBooks.OrderProcessing.Integrations;

internal class CreateOrderHandler(IOrderRepository orderRepository) : IRequestHandler<CreateOrderCommand, Result<OrderDetailsResponse>>
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

        // TODO: Retrieve real addresses
        var shippingAddress = new Address("123 Main", "", "Kent", "OH", "44444", "USA");
        var billingAddress = shippingAddress;

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
