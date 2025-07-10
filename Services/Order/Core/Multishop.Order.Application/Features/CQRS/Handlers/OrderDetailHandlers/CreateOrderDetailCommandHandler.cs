using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
public class CreateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
{
    private readonly IRepository<OrderDetail> _repository = repository;

    public async Task<bool> Handle(CreateOrderDetailCommand command)
    {
        var orderDetail = new OrderDetail
        {
            ProductId = command.ProductId,
            ProductName = command.ProductName,
            ProductPrice = command.ProductPrice,
            ProductAmount = command.ProductAmount,
            ProductTotalPrice = command.ProductTotalPrice,
            OrderingId = command.OrderingId
        };
        await _repository.CreateAsync(orderDetail);
        return true;
    }
}
