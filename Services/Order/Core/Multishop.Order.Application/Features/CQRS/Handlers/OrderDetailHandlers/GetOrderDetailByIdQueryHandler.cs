using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;

public class GetOrderDetailByIdQueryHandler(IRepository<OrderDetail> repository)
{
    private readonly IRepository<OrderDetail> _repository = repository;

    public async Task<GetOrderDetailByIdQueryResult> Handle(GetOrderDetailByIdQuery query)
    {
        var orderDetail = await _repository.GetByIdAsync(query.Id);
        if (orderDetail == null)
        {
            return null; // or throw an exception
        }
        
        return new GetOrderDetailByIdQueryResult
        {
            OrderDetailId = orderDetail.OrderDetailId,
            ProductId = orderDetail.ProductId,
            ProductName = orderDetail.ProductName,
            ProductPrice = orderDetail.ProductPrice,
            ProductAmount = orderDetail.ProductAmount,
            ProductTotalPrice = orderDetail.ProductTotalPrice,
            OrderingId = orderDetail.OrderingId
        };
    }
}
