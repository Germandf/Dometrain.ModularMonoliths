using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases;
using System.Security.Claims;

namespace RiverBooks.Users.Endpoints;

public record CheckoutRequest(Guid ShippingAddressId, Guid BillingAddressId);

public record CheckoutResponse(Guid OrderId);

internal class Checkout(IMediator mediator) : Endpoint<CheckoutRequest, CheckoutResponse>
{
    public override void Configure()
    {
        Post("/cart-items/checkout");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CheckoutRequest req, CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress")!;
        var command = new CheckoutCommand(emailAddress, req.ShippingAddressId, req.BillingAddressId);
        var result = await mediator.Send(command, ct);
        if (result.Status is ResultStatus.Unauthorized)
            await SendUnauthorizedAsync();
        else
            await SendOkAsync(new CheckoutResponse(result.Value), ct);
    }
}
