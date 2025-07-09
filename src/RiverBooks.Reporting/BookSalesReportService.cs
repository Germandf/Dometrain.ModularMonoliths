using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace RiverBooks.Reporting;

internal class BookSalesReportService(IConfiguration configuration) : IBookSalesReportService
{
    public async Task<TopBooksByMonthReport> GetByMonthAsync(int year, int month, CancellationToken ct)
    {
        using var dbConnection = new SqlConnection(configuration.GetConnectionString("ReportingConnectionString"));
        var sql = @"
            select BookId, Title, Author, UnitsSold as Units, TotalSales as Sales
            from Reporting.MonthlyBookSales
            where Month = @month and Year = @year
            ORDER BY TotalSales DESC";
        var results = (await dbConnection.QueryAsync<BookSalesResult>(sql, new { month, year })).ToList();
        var report = new TopBooksByMonthReport(
            year,
            month,
            CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
            results.ToList());
        return report;
    }
}
