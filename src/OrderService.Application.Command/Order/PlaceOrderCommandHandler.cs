using Mapster;
using Mediator;
using OrderService.Domain.Common;
using OrderService.Domain.DomainService;
using OrderService.Domain.Order.Dto;
using OrderService.Domain.Order.Exceptions;

namespace OrderService.Application.Command.Order;

public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, Guid>
{
    private readonly ICustomerDomainService _customerDomainService;
    private readonly IProductDomainService _productDomainService;

    public PlaceOrderCommandHandler(
        ICustomerDomainService customerDomainService,
        IProductDomainService productDomainService
    )
    {
        _customerDomainService = customerDomainService;
        _productDomainService = productDomainService;
    }

    public async ValueTask<Guid> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        var isCustomerExists = await _customerDomainService.IsCustomerExistsAsync(request.CustomerId);
        Guard.Assert<CustomerNotFoundException>(isCustomerExists is false);

        var validProductIds = await _productDomainService.FilterOutValidProductIdsAsync(request.ProductIds);
        var invalidProductIds = request.ProductIds.Except(validProductIds).ToArray();
        Guard.Assert(invalidProductIds.Length > 0, new ProductsNotFoundException(invalidProductIds));

        var placeDto = request.Adapt<PlaceDto>();

        var orderModel = Domain.Order.Order.Place(placeDto);

        return orderModel.Id;
    }
}