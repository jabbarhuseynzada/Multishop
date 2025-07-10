using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers;
public class UpdateOrderingCommandHandler(IRepository<Ordering> repository) : IRequestHandler<UpdateOrderingCommand>
{
    private readonly IRepository<Ordering> _repository = repository;

    public async Task Handle(UpdateOrderingCommand request, CancellationToken cancellationToken)
    {
        var ordering = await _repository.GetByIdAsync(request.OrderingId);
        if (ordering == null)
        {
            throw new KeyNotFoundException($"Ordering with ID {request.OrderingId} not found.");
        }
        ordering.UserId = request.UserId;
        ordering.TotalPrice = request.TotalPrice;
        ordering.OrderDate = request.OrderDate;
        await _repository.UpdateAsync(ordering);
    }
}
