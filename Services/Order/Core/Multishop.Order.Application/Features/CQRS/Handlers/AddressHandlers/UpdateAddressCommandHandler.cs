using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
public class UpdateAddressCommandHandler(IRepository<Address> repository)
{
    private readonly IRepository<Address> _repository = repository;
    public async Task Handle(UpdateAddressCommand command)
    {
        var address = await _repository.GetByIdAsync(command.AddressId);
        if (address == null)
        {
            throw new KeyNotFoundException($"Address with ID {command.AddressId} not found.");
        }
        address.UserId = command.UserId;
        address.District = command.District;
        address.City = command.City;
        address.Detail = command.Detail;
        await _repository.UpdateAsync(address);
    }
}
