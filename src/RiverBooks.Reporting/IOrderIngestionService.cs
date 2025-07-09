namespace RiverBooks.Reporting;

internal interface IOrderIngestionService
{
    Task AddOrUpdateBookSalesAsync(BookSales sales, CancellationToken cancellationToken);
}
