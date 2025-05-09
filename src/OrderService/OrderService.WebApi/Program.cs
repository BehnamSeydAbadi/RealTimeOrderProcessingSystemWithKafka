using Mapster;
using OrderService.Infrastructure;
using OrderService.WebApi.Order;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMapster();
builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);

OrderServiceInfrastructureBootstrapper.Run(builder.Services, builder.Configuration);

OrderMapper.Register();

var app = builder.Build();

EnsureOrderServiceDbContextCreated(app);
EnsureOutboxInboxDbContextCreated(app);

OrderEndpoints.Map(app);

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

return;


void EnsureOrderServiceDbContextCreated(WebApplication webApplication)
{
    var serviceScope = webApplication.Services.CreateScope();
    var orderServiceDbContext = serviceScope.ServiceProvider.GetRequiredService<OrderServiceDbContext>();
    orderServiceDbContext.Database.EnsureCreated();
}

void EnsureOutboxInboxDbContextCreated(WebApplication webApplication)
{
    var serviceScope = webApplication.Services.CreateScope();
    var orderServiceDbContext = serviceScope.ServiceProvider.GetRequiredService<OutboxInboxDbContext>();
    orderServiceDbContext.Database.EnsureCreated();
}