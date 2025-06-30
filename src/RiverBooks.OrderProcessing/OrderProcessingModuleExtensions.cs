using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.OrderProcessing.Data;
using Serilog;
using System.Reflection;

namespace RiverBooks.OrderProcessing;

public static class OrderProcessingModuleExtensions
{
    public static IServiceCollection AddOrderProcessingModuleServices(
        this IServiceCollection services,
        IConfiguration configuration,
        ILogger logger,
        List<Assembly> mediatRAssemblies)
    {
        services.AddDbContext<OrderProcessingDbContext>(o => o
            .UseSqlServer(configuration.GetConnectionString("OrderProcessingConnectionString")));
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IAddressCache, AddressCache>();
        mediatRAssemblies.Add(typeof(OrderProcessingModuleExtensions).Assembly);
        logger.Information("{Module} module services registered", "OrderProcessing");
        return services;
    }
}
