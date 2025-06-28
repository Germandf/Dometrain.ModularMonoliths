using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Contracts;

namespace RiverBooks.Users.UseCases;

public record CheckoutCommand(string EmailAddress, Guid ShippingAddressId, Guid BillingAddressId)
    : IRequest<Result<Guid>>;

internal class CheckoutHandler(IApplicationUserRepository userRepository, IMediator mediator)
    : IRequestHandler<CheckoutCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CheckoutCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);
        if (user is null)
            return Result.Unauthorized();

        var items = user.CartItems
            .Select(item => new OrderItemDetail(
                item.BookId,
                item.Quantity,
                item.UnitPrice,
                item.Description))
            .ToList();

        var createOrderCommand = new CreateOrderCommand(
            Guid.Parse(user.Id),
            request.ShippingAddressId,
            request.BillingAddressId,
            items);

        var result = await mediator.Send(createOrderCommand, cancellationToken);
        if (!result.IsSuccess)
            return result.Map(x => x.OrderId);

        user.ClearCart();
        await userRepository.SaveChangesAsync();
        return Result.Success(result.Value.OrderId);
    }
}
