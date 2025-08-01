﻿using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
public class RemoveOrderDetailCommandHandler
{
    private readonly IRepository<OrderDetail> _repository;
    public RemoveOrderDetailCommandHandler(IRepository<OrderDetail> repository) => _repository = repository;
    public async Task<bool> Handle(RemoveOrderDetailCommand command)
    {
        var orderDetail = await _repository.GetByIdAsync(command.Id);
        if (orderDetail == null)
        {
            return false; // or throw an exception
        }
        await _repository.DeleteAsync(orderDetail);
        return true;
    }
}
