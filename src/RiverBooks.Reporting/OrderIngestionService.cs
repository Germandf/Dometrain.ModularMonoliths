using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace RiverBooks.Reporting;

internal class OrderIngestionService(IConfiguration configuration) : IOrderIngestionService
{
    public async Task AddOrUpdateBookSalesAsync(BookSales bookSales, CancellationToken cancellationToken)
    {
        using var dbConnection = new SqlConnection(configuration.GetConnectionString("ReportingConnectionString"));
        var sql = @"
            IF EXISTS (SELECT 1 FROM Reporting.MonthlyBookSales WHERE BookId = @BookId AND Year = @Year AND Month = @Month)
            BEGIN
                UPDATE Reporting.MonthlyBookSales
                SET UnitsSold = UnitsSold + @UnitsSold, TotalSales = TotalSales + @TotalSales
                WHERE BookId = @BookId AND Year = @Year AND Month = @Month
            END
            ELSE
            BEGIN
                INSERT INTO Reporting.MonthlyBookSales (BookId, Title, Author, Year, Month, UnitsSold, TotalSales)
                VALUES (@BookId, @Title, @Author, @Year, @Month, @UnitsSold, @TotalSales)
            END";
        await dbConnection.ExecuteAsync(sql, new
        {
            bookSales.BookId,
            bookSales.Title,
            bookSales.Author,
            bookSales.Year,
            bookSales.Month,
            bookSales.UnitsSold,
            bookSales.TotalSales
        });
    }
}
