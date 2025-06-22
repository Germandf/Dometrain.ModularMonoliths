using FastEndpoints;
using FastEndpoints.Swagger;
using RiverBooks.Books;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddOpenApi();
builder.Services.AddBookServices(builder.Configuration);
builder.Services.AddFastEndpoints()
    .SwaggerDocument();

var app = builder.Build();

//app.MapOpenApi();
//app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "Demo Api"));
app.UseHttpsRedirection();
app.UseFastEndpoints()
   .UseSwaggerGen();

app.Run();

public partial class Program { }
