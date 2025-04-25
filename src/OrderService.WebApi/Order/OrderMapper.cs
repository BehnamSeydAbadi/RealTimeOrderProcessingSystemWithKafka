using Mapster;
using OrderService.Application.Command.Order;
using OrderService.Domain.Order.Dto;
using OrderService.Domain.Order.Events;
using OrderService.Infrastructure.Order;

namespace OrderService.WebApi.Order;

public class OrderMapper
{
    public static void Register()
    {
        TypeAdapterConfig<OrderEntity, Domain.Order.Order>.NewConfig();
        TypeAdapterConfig<PlaceOrderCommand, PlaceDto>.NewConfig();
        TypeAdapterConfig<OrderPlacedEvent, OrderEntity>.NewConfig();
    }
}