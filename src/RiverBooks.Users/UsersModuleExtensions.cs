using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Infrastructure.Data;
using RiverBooks.Users.Interfaces;
using Serilog;
using System.Reflection;

namespace RiverBooks.Users;

public static class UsersModuleExtensions
{
    public static IServiceCollection AddUsersModuleServices(
        this IServiceCollection services,
        IConfiguration configuration,
        ILogger logger,
        List<Assembly> mediatRAssemblies)
    {
        services.AddDbContext<UsersDbContext>(o => o
            .UseSqlServer(configuration.GetConnectionString("UsersConnectionString")));
        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<UsersDbContext>();
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddScoped<IReadOnlyUserAddressRepository, ReadOnlyUserAddressRepository>();
        mediatRAssemblies.Add(typeof(UsersModuleExtensions).Assembly);
        logger.Information("{Module} module services registered", "Users");
        return services;
    }
}
