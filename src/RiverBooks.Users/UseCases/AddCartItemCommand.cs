using Ardalis.Result;
using FluentValidation;
using MediatR;
using RiverBooks.Books.Contracts;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.UseCases;

public record AddCartItemCommand(Guid BookId, int Quantity, string EmailAddress)
    : IRequest<Result>;

public class AddCartItemValidator : AbstractValidator<AddCartItemCommand>
{
    public AddCartItemValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty()
            .WithMessage("Book ID is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");

        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("A valid email address is required.");
    }
}

internal class AddCartItemHandler(IApplicationUserRepository repository, IMediator mediator)
    : IRequestHandler<AddCartItemCommand, Result>
{
    public async Task<Result> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetUserWithCartByEmailAsync(request.EmailAddress);
        if (user is null)
            return Result.Unauthorized();

        var bookResult = await mediator.Send(new GetBookDetailsQuery(request.BookId), cancellationToken);
        if (bookResult.IsSuccess is false)
            return Result.NotFound();

        var book = bookResult.Value;
        var description = $"{book.Title} by {book.Author}";
        var newCartItem = new CartItem(request.BookId, description, request.Quantity, book.Price);
        user.AddItemToCart(newCartItem);
        await repository.SaveChangesAsync();
        return Result.Success();
    }
}
