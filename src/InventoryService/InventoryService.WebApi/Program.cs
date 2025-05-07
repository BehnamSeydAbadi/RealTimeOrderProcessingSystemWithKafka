using InventoryService.Infrastructure;
using InventoryService.WebApi.Inventory;
using Mapster;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMapster();
builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
InventoryServiceInfrastructureBootstrapper.Run(builder.Services);

InventoryMapper.Register();
ProductMapper.Register();

var app = builder.Build();

EnsureDatabaseCreated(app);

InventoryEndpoints.Map(app);

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

void EnsureDatabaseCreated(WebApplication webApplication)
{
    var serviceScope = webApplication.Services.CreateScope();
    var orderServiceDbContext = serviceScope.ServiceProvider.GetRequiredService<InventoryServiceDbContext>();
    orderServiceDbContext.Database.EnsureCreated();
}