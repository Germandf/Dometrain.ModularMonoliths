using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Reporting.Endpoints;
using Serilog;
using System.Data;
using System.Reflection;

namespace RiverBooks.Reporting;

public static class ReportingModuleExtensions
{
    public static IServiceCollection AddReportingModuleServices(
        this IServiceCollection services,
        IConfiguration configuration,
        ILogger logger,
        List<Assembly> mediatRAssemblies)
    {
        services.AddScoped<ITopSellingBooksReportService, TopSellingBooksReportService>();
        services.AddScoped<IDbConnection>(sp =>
        {
            var connectionString = configuration.GetConnectionString("OrderProcessingConnectionString");
            var dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
            return dbConnection;
        });
        mediatRAssemblies.Add(typeof(ReportingModuleExtensions).Assembly);
        logger.Information("{Module} module services registered", "Reporting");
        return services;
    }
}
