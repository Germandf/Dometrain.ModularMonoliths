using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace RiverBooks.Reporting;

public class DatabaseInitializerBackgroundService(IConfiguration configuration) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connectionString = configuration.GetConnectionString("ReportingConnectionString");
        var builder = new SqlConnectionStringBuilder(connectionString);
        var databaseName = builder.InitialCatalog;
        var masterConnectionString = new SqlConnectionStringBuilder(connectionString)
        {
            InitialCatalog = "master"
        }.ConnectionString;
        using (var masterConnection = new SqlConnection(masterConnectionString))
        {
            await masterConnection.OpenAsync(stoppingToken);
            var cmd = masterConnection.CreateCommand();
            cmd.CommandText = @"
                IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = @dbName)
                BEGIN
                    CREATE DATABASE [" + databaseName + @"]
                END";
            cmd.Parameters.AddWithValue("@dbName", databaseName);
            await cmd.ExecuteNonQueryAsync(stoppingToken);
        }
        using (var reportingConnection = new SqlConnection(connectionString))
        {
            await reportingConnection.OpenAsync(stoppingToken);
            var ensureTableSql = @"
                IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Reporting')
                BEGIN
                    EXEC('CREATE SCHEMA Reporting')
                END
                IF NOT EXISTS (
                    SELECT * FROM sys.tables t 
                    JOIN sys.schemas s ON t.schema_id = s.schema_id
                    WHERE t.name = 'MonthlyBookSales' AND s.name = 'Reporting' AND t.type = 'U')
                BEGIN
                    CREATE TABLE Reporting.MonthlyBookSales
                    (
                        BookId uniqueidentifier,
                        Title NVARCHAR(255),
                        Author NVARCHAR(255),
                        Year INT,
                        Month INT,
                        UnitsSold INT,
                        TotalSales DECIMAL(18, 2),
                        PRIMARY KEY (BookId, Year, Month)
                    );
                END";
            await reportingConnection.ExecuteAsync(new CommandDefinition(ensureTableSql, cancellationToken: stoppingToken));
        }
    }
}
