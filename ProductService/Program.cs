var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();


app.UseHttpsRedirection();

app.MapGet("/api/products", () =>
{
    return new[] { "Product1", "Product2" };
});

app.Run("https://localhost:5002");