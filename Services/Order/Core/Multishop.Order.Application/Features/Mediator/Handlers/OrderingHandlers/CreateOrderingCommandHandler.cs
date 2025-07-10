using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System.Runtime.CompilerServices;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers;
public class CreateOrderingCommandHandler(IRepository<Ordering> repository) : IRequestHandler<CreateOrderingCommand>
{
    private readonly IRepository<Ordering> _repository = repository;

    public async Task Handle(CreateOrderingCommand request, CancellationToken cancellationToken)
    {
        await _repository.CreateAsync(new Ordering
        {
            UserId = request.UserId,
            TotalPrice = request.TotalPrice,
            OrderDate = DateTime.UtcNow
        });

    }
}
