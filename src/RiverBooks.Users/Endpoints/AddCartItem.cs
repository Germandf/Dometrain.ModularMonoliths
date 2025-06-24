using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases;
using System.Security.Claims;

namespace RiverBooks.Users.Endpoints;

public record AddCartItemRequest(Guid BookId, int Quantity);

internal class AddCartItem(IMediator mediator) : Endpoint<AddCartItemRequest>
{
    public override void Configure()
    {
        Post("/cart-items");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(AddCartItemRequest req, CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress")!;
        var command = new AddCartItemCommand(req.BookId, req.Quantity, emailAddress);
        var result = await mediator.Send(command, ct);
        if (result.Status is ResultStatus.Unauthorized)
            await SendUnauthorizedAsync();
        else
            await SendOkAsync();
    }
}
