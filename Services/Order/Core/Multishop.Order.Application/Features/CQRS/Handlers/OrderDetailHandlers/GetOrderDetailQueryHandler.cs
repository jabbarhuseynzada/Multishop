using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
public class GetOrderDetailQueryHandler(IRepository<OrderDetail> repository)
{
    private readonly IRepository<OrderDetail> _repository = repository;
    public async Task<List<GetOrderDetailQueryResult>> Handle()
    {
        var orderDetails = await _repository.GetAllAsync();
        var result = orderDetails.Select(od => new GetOrderDetailQueryResult
        {
            OrderDetailId = od.OrderDetailId,
            ProductId = od.ProductId,
            ProductName = od.ProductName,
            ProductPrice = od.ProductPrice,
            ProductAmount = od.ProductAmount,
            ProductTotalPrice = od.ProductTotalPrice,
            OrderingId = od.OrderingId,
        }).ToList();
        return result;
    }
}
