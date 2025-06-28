using Ardalis.Result;
using MediatR;

namespace RiverBooks.OrderProcessing.UseCases;

public record ListOrdersQuery(string EmailAddress)
    : IRequest<Result<List<OrderDto>>>;

internal class ListOrdersHandler(IOrderRepository repository)
    : IRequestHandler<ListOrdersQuery, Result<List<OrderDto>>>
{
    public async Task<Result<List<OrderDto>>> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
    {
        // TODO: get user id and filter by user
        var orders = await repository.ListAsync();
        return orders
            .Select(i => new OrderDto(i.Id, i.UserId, i.DateCreated, null, i.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice)))
            .ToList();
    }
}
