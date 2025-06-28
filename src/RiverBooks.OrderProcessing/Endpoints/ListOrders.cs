using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.OrderProcessing.UseCases;
using System.Security.Claims;

namespace RiverBooks.OrderProcessing.Endpoints;

public record ListOrdersResponse(List<OrderDto> Orders);

internal class ListOrders(IMediator mediator) : EndpointWithoutRequest<ListOrdersResponse>
{
    public override void Configure()
    {
        Get("/orders");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress")!;
        var query = new ListOrdersQuery(emailAddress);
        var result = await mediator.Send(query, ct);
        if (result.Status is ResultStatus.Unauthorized)
            await SendUnauthorizedAsync();
        else
            await SendOkAsync(new ListOrdersResponse(result.Value), cancellation: ct);
    }
}
