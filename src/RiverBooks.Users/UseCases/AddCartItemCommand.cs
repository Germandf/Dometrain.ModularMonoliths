using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users.UseCases;

public record AddCartItemCommand(Guid BookId, int Quantity, string EmailAddress)
    : IRequest<Result>;

internal class AddCartItemHandler(IApplicationUserRepository repository)
    : IRequestHandler<AddCartItemCommand, Result>
{
    public async Task<Result> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetUserWithCartByEmailAsync(request.EmailAddress);
        if (user is null)
            return Result.Unauthorized();

        // TODO: Get book details from Books Module
        var newCartItem = new CartItem(request.BookId, "description", request.Quantity, 1.00m);
        user.AddItemToCart(newCartItem);
        await repository.SaveChangesAsync();
        return Result.Success();
    }
}
