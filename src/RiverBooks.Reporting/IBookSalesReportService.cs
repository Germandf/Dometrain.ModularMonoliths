namespace RiverBooks.Reporting;

internal interface IBookSalesReportService
{
    Task<TopBooksByMonthReport> GetByMonthAsync(int year, int month, CancellationToken ct);
}
