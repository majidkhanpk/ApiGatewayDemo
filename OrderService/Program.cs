using OrderService.Middleware;
using OrderService.RabbitMQ;
using OrderService.Repositories;
using OrderService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<RabbitMQConnection>();
builder.Services.AddSingleton<RabbitPublisher>();
builder.Services.AddScoped<IOrderMgrService, OrderMgrService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddControllers();


var app = builder.Build();
//app.UseMiddleware<CorrelationIdReaderMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run("https://localhost:5001");