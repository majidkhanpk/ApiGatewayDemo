var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("/api/orders", () =>
{
    return new[] { "Order1", "Order2" };
});

app.Run("https://localhost:5001");

