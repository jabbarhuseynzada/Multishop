using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
public class RemoveAddressCommandHandler(IRepository<Address> repository)
{
    private readonly IRepository<Address> _repository = repository;

    public async Task Handle(RemoveAddressCommand command)
    {
        var address = await _repository.GetByIdAsync(command.Id);
        if (address == null)
        {
            throw new KeyNotFoundException($"Address with ID {command.Id} not found.");
        }
        await _repository.DeleteAsync(address);
    }
}
