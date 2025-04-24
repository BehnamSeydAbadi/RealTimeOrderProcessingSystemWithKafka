namespace OrderService.Domain;

public class Order
{
    public static Order Place() => new() { Status = OrderStatus.Pending };
    
    public OrderStatus Status { get; private set; }
}