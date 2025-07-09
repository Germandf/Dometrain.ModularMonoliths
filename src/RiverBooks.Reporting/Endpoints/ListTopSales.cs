using Dapper;
using FastEndpoints;
using System.Data;
using System.Globalization;

namespace RiverBooks.Reporting.Endpoints;

internal record ListTopSalesRequest(int Year, int Month);

internal record ListTopSalesResponse(TopBooksByMonthReport Report);

internal class ListTopSales(ITopSellingBooksReportService reportService)
    : Endpoint<ListTopSalesRequest, ListTopSalesResponse>
{
    public override void Configure()
    {
        Get("/top-sales");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ListTopSalesRequest req, CancellationToken ct)
    {
        var report = await reportService.ReachInSqlQuery(req.Year, req.Month, ct);
        var response = new ListTopSalesResponse(report);
        await SendOkAsync(response, ct);
    }
}

internal interface ITopSellingBooksReportService
{
    Task<TopBooksByMonthReport> ReachInSqlQuery(int year, int month, CancellationToken ct);
}

internal class TopSellingBooksReportService(IDbConnection dbConnection) : ITopSellingBooksReportService
{
    public async Task<TopBooksByMonthReport> ReachInSqlQuery(int year, int month, CancellationToken ct)
    {
        var sql = @"
            SELECT 
                b.Id, b.Title, b.Author, 
                SUM(oi.Quantity) AS Units, 
                SUM(oi.Quantity * oi.UnitPrice) AS Sales
            FROM RiverBooks.Books.Books b
            JOIN RiverOrderProcessing.OrderProcessing.OrderItem oi ON b.Id = oi.BookId
            JOIN RiverOrderProcessing.OrderProcessing.Orders o ON oi.OrderId = o.Id
            WHERE MONTH(o.DateCreated) = @Month AND YEAR(o.DateCreated) = @Year
            GROUP BY b.Id, b.Title, b.Author
            ORDER BY Sales DESC";
        var results = await dbConnection.QueryAsync<BookSalesResult>(sql, new { Year = year, Month = month });
        return new TopBooksByMonthReport(year, month, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month), results.ToList());
    }
}

internal record TopBooksByMonthReport(
    int Year,
    int Month,
    string MonthName,
    List<BookSalesResult> Results);

internal record BookSalesResult(
    Guid Id,
    string Title,
    string Author,
    int Units,
    decimal Sales);
