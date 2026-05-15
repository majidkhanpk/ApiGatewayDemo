using ApiGateway.Middleware;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Load JSON config
builder.Configuration.AddJsonFile("ocelot.json");

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<CorrelationIdMiddleware>();

// Use Ocelot
await app.UseOcelot();

app.Run("https://localhost:5000");