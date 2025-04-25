using Mapster;
using OrderService.Infrastructure;
using OrderService.WebApi.Order;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMapster();
builder.Services.AddMediator();

InfrastructureBootstrapper.Run(builder.Services);

OrderMapper.Register();

var app = builder.Build();

EnsureDatabaseCreated(app);

app.UseSwagger();
app.UseSwaggerUI();

app.Run();


void EnsureDatabaseCreated(WebApplication webApplication)
{
    var serviceScope = webApplication.Services.CreateScope();
    var orderServiceDbContext = serviceScope.ServiceProvider.GetRequiredService<OrderServiceDbContext>();
    orderServiceDbContext.Database.EnsureCreated();
}