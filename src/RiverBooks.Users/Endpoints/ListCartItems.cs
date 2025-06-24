using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases;
using System.Security.Claims;

namespace RiverBooks.Users.Endpoints;

public record ListCartItemsResponse(List<CartItemDto> CartItems);

internal class ListCartItems(IMediator mediator) : EndpointWithoutRequest<ListCartItemsResponse>
{
    public override void Configure()
    {
        Get("/cart-items");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress")!;
        var query = new ListCartItemsQuery(emailAddress);
        var result = await mediator.Send(query, ct);
        if (result.Status is ResultStatus.Unauthorized)
            await SendUnauthorizedAsync();
        else
            await SendOkAsync(new ListCartItemsResponse(result.Value), cancellation: ct);
    }
}
