using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;

namespace MultiShop.Order.WebApi.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AddressController(
    GetAddressQueryHandler getAddressQueryHandler,
    GetAddressByIdQueryHandler getAddressByIdQueryHandler,
    CreateAddressCommandHandler createAddressCommandHandler,
    UpdateAddressCommandHandler updateAddressCommandHandler,
    RemoveAddressCommandHandler deleteAddressCommandHandler) : ControllerBase
{
    private readonly GetAddressQueryHandler _getAddressQueryHandler = getAddressQueryHandler;
    private readonly GetAddressByIdQueryHandler _getAddressByIdQueryHandler = getAddressByIdQueryHandler;
    private readonly CreateAddressCommandHandler _createAddressCommandHandler = createAddressCommandHandler;
    private readonly UpdateAddressCommandHandler _updateAddressCommandHandler = updateAddressCommandHandler;
    private readonly RemoveAddressCommandHandler _deleteAddressCommandHandler = deleteAddressCommandHandler;

    [HttpGet]
    public async Task<IActionResult> AddressList()
    {
        var result = await _getAddressQueryHandler.Handle();
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAddressById(int id)
    {

        var result = await _getAddressByIdQueryHandler.Handle(new GetAddressByIdQuery(id));
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateAddress([FromBody] CreateAddressCommand command)
    {
        if (command == null)
        {
            return BadRequest("Invalid address data.");
        }
        await _createAddressCommandHandler.Handle(command);
        return Ok("Created Successfully");
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressCommand command)
    {
        if (command == null)
        {
            return BadRequest("Invalid address data.");
        }
        await _updateAddressCommandHandler.Handle(command);
        return Ok("Updated Successfully");
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid address ID.");
        }
        await _deleteAddressCommandHandler.Handle(new RemoveAddressCommand(id));
        return Ok("Deleted Successfully");
    }
}
