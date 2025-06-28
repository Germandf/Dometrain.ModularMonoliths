using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases;
using System.Security.Claims;

namespace RiverBooks.Users.Endpoints;

public record ListAddressesResponse(IEnumerable<UserAddressDto> Addresses);

public record UserAddressDto(
    Guid id,
    string Street1,
    string Street2,
    string City,
    string State,
    string PostalCode,
    string Country);

internal class ListAddresses(IMediator mediator) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/addresses");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress")!;
        var query = new ListAddressesQuery(emailAddress);
        var result = await mediator.Send(query, ct);
        if (result.Status is ResultStatus.Unauthorized)
            await SendUnauthorizedAsync();
        else
            await SendOkAsync(result.Value);
    }
}
