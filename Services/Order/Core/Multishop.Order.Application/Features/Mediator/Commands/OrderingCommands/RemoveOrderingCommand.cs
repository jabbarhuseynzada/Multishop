using MediatR;

namespace MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
public class RemoveOrderingCommand(int orderingId) : IRequest
{
    public int OrderingId { get; set; } = orderingId;
}
