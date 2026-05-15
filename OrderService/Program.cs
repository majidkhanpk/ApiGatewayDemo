using OrderService.Middleware;
using OrderService.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<RabbitPublisher>();
builder.Services.AddControllers();

var app = builder.Build();
//app.UseMiddleware<CorrelationIdReaderMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
//app.MapGet("/api/orders", () =>
//{
//    return new[] { "Order1", "Order2" };
//});

app.Run("https://localhost:5001");