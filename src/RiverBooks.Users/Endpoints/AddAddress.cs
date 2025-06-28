using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases;
using System.Security.Claims;

namespace RiverBooks.Users.Endpoints;

public record AddAddressRequest(
    string Street1,
    string Street2,
    string City,
    string State,
    string PostalCode,
    string Country);

internal class AddAddress(IMediator mediator) : Endpoint<AddAddressRequest>
{
    public override void Configure()
    {
        Post("/addresses");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(AddAddressRequest req, CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress")!;
        var command = new AddAddressCommand(emailAddress, req.Street1, req.Street2, req.City, req.State, req.PostalCode, req.Country);
        var result = await mediator.Send(command, ct);
        if (result.Status is ResultStatus.Unauthorized)
            await SendUnauthorizedAsync();
        else
            await SendOkAsync();
    }
}
