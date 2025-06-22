using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Users.Data;
using Serilog;

namespace RiverBooks.Users;

public static class UsersModuleExtensions
{
    public static IServiceCollection AddUsersModuleServices(
        this IServiceCollection services,
        IConfiguration configuration,
        ILogger logger)
    {
        services.AddDbContext<UsersDbContext>(o => o
            .UseSqlServer(configuration.GetConnectionString("UsersConnectionString")));
        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<UsersDbContext>();
        logger.Information("{Module} module services registered", "Users");
        return services;
    }
}
