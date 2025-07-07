using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace RiverBooks.EmailSending;

public static class EmailSendingModuleExtensions
{
    public static IServiceCollection AddEmailSendingModuleServices(
        this IServiceCollection services,
        IConfiguration configuration,
        ILogger logger,
        List<Assembly> mediatRAssemblies)
    {
        services.AddTransient<IEmailSender, MimeKitEmailSender>();
        mediatRAssemblies.Add(typeof(EmailSendingModuleExtensions).Assembly);
        logger.Information("{Module} module services registered", "EmailSending");
        return services;
    }
}
