using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users.UseCases;

public record ListCartItemsQuery(string EmailAddress)
    : IRequest<Result<List<CartItemDto>>>;

internal class ListCartItemsHandler(IApplicationUserRepository repository)
    : IRequestHandler<ListCartItemsQuery, Result<List<CartItemDto>>>
{
    public async Task<Result<List<CartItemDto>>> Handle(ListCartItemsQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.GetUserWithCartByEmailAsync(request.EmailAddress);
        if (user is null)
            return Result.Unauthorized();

        return user.CartItems
            .Select(i => new CartItemDto(i.Id, i.BookId, i.Description, i.Quantity, i.UnitPrice))
            .ToList();
    }
}
