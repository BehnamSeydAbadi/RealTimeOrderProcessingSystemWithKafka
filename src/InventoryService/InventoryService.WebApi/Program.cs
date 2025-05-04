using InventoryService.WebApi.Inventory;
using Mapster;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMapster();
builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);

InventoryMapper.Register();
ProductMapper.Register();

var app = builder.Build();

InventoryEndpoints.Map(app);

app.UseSwagger();
app.UseSwaggerUI();

app.Run();