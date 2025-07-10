using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers;
public class RemoveOrderingCommandHandler(IRepository<Ordering> repository) : IRequestHandler<RemoveOrderingCommand>
{
    private readonly IRepository<Ordering> _repository = repository;

    public async Task Handle(RemoveOrderingCommand request, CancellationToken cancellationToken)
    {
        var ordering = await _repository.GetByIdAsync(request.OrderingId);
        if (ordering == null)
        {
            throw new KeyNotFoundException($"Ordering with ID {request.OrderingId} not found.");
        }
        await _repository.DeleteAsync(ordering);
    }
}
