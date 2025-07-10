using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
public class CreateAddressCommandHandler(IRepository<Address> repository)
{
    private readonly IRepository<Address> _repository = repository;

    public async Task Handle (CreateAddressCommand command)
    {
        var address = new Address
        {
            UserId = command.UserId,
            District = command.District,
            City = command.City,
            Detail = command.Detail
        };
        await _repository.CreateAsync(address);
    }
}
