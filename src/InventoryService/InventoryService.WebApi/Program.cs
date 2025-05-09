using InventoryService.Application.Command;
using InventoryService.Infrastructure;
using InventoryService.WebApi.BackgroundServices;
using InventoryService.WebApi.Inventory;
using Mapster;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMapster();
builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);

ApplicationCommandsBootstrapper.Run(builder.Services);
InfrastructureBootstrapper.Run(builder.Services, builder.Configuration);

builder.Services.AddHostedService<OrderPlacedSubscriberBackgroundService>();

InventoryMapper.Register();
ProductMapper.Register();

var app = builder.Build();

EnsureInventoryServiceDbContextCreated(app);
EnsureOutboxInboxDbContextCreated(app);

InventoryEndpoints.Map(app);

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

return;

void EnsureInventoryServiceDbContextCreated(WebApplication webApplication)
{
    var serviceScope = webApplication.Services.CreateScope();
    var orderServiceDbContext = serviceScope.ServiceProvider.GetRequiredService<InventoryServiceDbContext>();
    orderServiceDbContext.Database.EnsureCreated();
}

void EnsureOutboxInboxDbContextCreated(WebApplication webApplication)
{
    var serviceScope = webApplication.Services.CreateScope();
    var orderServiceDbContext = serviceScope.ServiceProvider.GetRequiredService<OutboxInboxDbContext>();
    orderServiceDbContext.Database.EnsureCreated();
}