using ProductService.Middleware;
using ProductService.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//builder.Services.AddHostedService<OrderConsumer>();
builder.Services.AddControllers();

var app = builder.Build();
app.UseMiddleware<CorrelationIdReaderMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();
//app.MapGet("/api/products", () =>
//{
//    return new[] { "Product1", "Product2" };
//});

app.Run("https://localhost:5002");