using OrderService.Domain;
using OrderService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DomainBootstrapper.Run(builder.Services);
// ApplicationBootstrapper.Run(builder.Services);
InfrastructureBootstrapper.Run(builder.Services);

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