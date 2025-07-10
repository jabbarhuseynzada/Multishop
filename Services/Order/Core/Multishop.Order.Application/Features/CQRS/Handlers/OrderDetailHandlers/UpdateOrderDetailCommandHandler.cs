using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
public class UpdateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
{
    private readonly IRepository<OrderDetail> _repository = repository;

    public async Task<bool> Handle(UpdateOrderDetailCommand command)
    {
        var orderDetail = await _repository.GetByIdAsync(command.OrderDetailId);
        if (orderDetail == null)
        {
            return false; // or throw an exception
        }
        orderDetail.ProductId = command.ProductId;
        orderDetail.ProductName = command.ProductName;
        orderDetail.ProductPrice = command.ProductPrice;
        orderDetail.ProductAmount = command.ProductAmount;
        orderDetail.ProductTotalPrice = command.ProductTotalPrice;
        orderDetail.OrderingId = command.OrderingId;
        await _repository.UpdateAsync(orderDetail);
        return true;
    }
}
