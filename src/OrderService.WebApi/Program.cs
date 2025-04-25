using OrderService.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DomainBootstrapper.Run(builder.Services);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();