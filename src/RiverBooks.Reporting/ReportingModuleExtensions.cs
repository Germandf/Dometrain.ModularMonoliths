using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
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
        services.AddScoped<IBookSalesReportService, BookSalesReportService>();
        services.AddScoped<IOrderIngestionService, OrderIngestionService>();
        services.AddHostedService<DatabaseInitializerBackgroundService>();
        mediatRAssemblies.Add(typeof(ReportingModuleExtensions).Assembly);
        logger.Information("{Module} module services registered", "Reporting");
        return services;
    }
}
