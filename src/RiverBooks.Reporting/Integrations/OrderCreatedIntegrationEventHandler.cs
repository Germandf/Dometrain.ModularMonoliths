using MediatR;
using RiverBooks.Books.Contracts;
using RiverBooks.OrderProcessing.Contracts;

namespace RiverBooks.Reporting.Integrations;

internal class OrderCreatedIntegrationEventHandler(
    IMediator mediator,
    IOrderIngestionService orderIngestionService)
    : INotificationHandler<OrderCreatedIntegrationEvent>
{
    public async Task Handle(OrderCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var orderDetail = notification.OrderDetail;
        foreach (var item in orderDetail.OrderItems)
        {
            var bookDetailsQuery = new GetBookDetailsQuery(item.BookId);
            var bookResult = await mediator.Send(bookDetailsQuery, cancellationToken);
            if (bookResult.IsSuccess is false)
                continue;
            var book = bookResult.Value;
            var bookSales = new BookSales
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                Year = orderDetail.DateCreated.Year,
                Month = orderDetail.DateCreated.Month,
                TotalSales = item.Quantity * item.UnitPrice,
                UnitsSold = item.Quantity
            };
            await orderIngestionService.AddOrUpdateBookSalesAsync(bookSales, cancellationToken);
        }
    }
}
