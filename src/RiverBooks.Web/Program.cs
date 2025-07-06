using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using NJsonSchema;
using RiverBooks.Books;
using RiverBooks.OrderProcessing;
using RiverBooks.SharedKernel;
using RiverBooks.Users;
using Serilog;
using System.Reflection;

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, _, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));
builder.Services.AddAuthenticationJwtBearer(o => o.SigningKey = builder.Configuration["Auth:JwtSecret"]);
builder.Services.AddAuthorization();
//builder.Services.AddOpenApi();
builder.Services.AddFastEndpoints().SwaggerDocument();
List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];
builder.Services.AddBooksModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddOrderProcessingModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddUsersModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray()));
builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

var app = builder.Build();

//app.MapOpenApi();
//app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "Demo Api"));
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints().UseSwaggerGen(
    settings =>
    {
        settings.PostProcess = (doc, req) =>
        {
            foreach (var schema in doc.Definitions.Values)
                foreach (var prop in schema.Properties.Values)
                    if (prop.Type is JsonObjectType.String && prop.Format is JsonFormatStrings.Guid && prop.Example is null)
                        prop.Example = Guid.NewGuid().ToString();
        };
    },
    settings =>
    {
        settings.DocExpansion = "list";
    });

app.Run();

public partial class Program { }
