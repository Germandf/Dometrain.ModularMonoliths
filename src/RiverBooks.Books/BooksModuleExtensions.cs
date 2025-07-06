using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Books.Infrastructure.Data;
using RiverBooks.Books.Interfaces;
using RiverBooks.Books.UseCases;
using Serilog;
using System.Reflection;

namespace RiverBooks.Books;

public static class BooksModuleExtensions
{
    public static IServiceCollection AddBooksModuleServices(
        this IServiceCollection services,
        IConfiguration configuration,
        ILogger logger,
        List<Assembly> mediatRAssemblies)
    {
        services.AddDbContext<BookDbContext>(o => o
            .UseSqlServer(configuration.GetConnectionString("BooksConnectionString")));
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookService, BookService>();
        mediatRAssemblies.Add(typeof(BooksModuleExtensions).Assembly);
        logger.Information("{Module} module services registered", "Books");
        return services;
    }
}
