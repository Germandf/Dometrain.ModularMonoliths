using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
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
        services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
        services.AddMongoDb(configuration);
        services.AddTransient<IEmailSender, MimeKitEmailSender>();
        services.AddTransient<IOutboxService, MongoDbOutboxService>();
        services.AddTransient<IOutboxEmailService, OutboxEmailService>();
        services.AddHostedService<EmailSendingBackgroundService>();
        mediatRAssemblies.Add(typeof(EmailSendingModuleExtensions).Assembly);
        logger.Information("{Module} module services registered", "EmailSending");
        return services;
    }

    public static IServiceCollection AddMongoDb(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            var settings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            return new MongoClient(settings!.ConnectionString);
        });
        services.AddSingleton(serviceProvider =>
        {
            var settings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            var client = serviceProvider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings!.DatabaseName);
        });
        services.AddTransient(serviceProvider =>
        {
            var database = serviceProvider.GetRequiredService<IMongoDatabase>();
            return database.GetCollection<EmailOutbox>(nameof(EmailOutbox));
        });
        return services;
    }
}
