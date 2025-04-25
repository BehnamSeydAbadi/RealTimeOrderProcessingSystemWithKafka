using Mediator;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Command.Order;

namespace OrderService.WebApi.Order;

public class OrderEndpoints
{
    public static void Map(WebApplication webApplication)
    {
        webApplication.MapPost("api/orders", async ([FromBody] PlaceOrderCommand command, IMediator mediator) =>
        {
            var orderId = await mediator.Send(command);
            return Results.Ok(orderId);
        });
    }
}