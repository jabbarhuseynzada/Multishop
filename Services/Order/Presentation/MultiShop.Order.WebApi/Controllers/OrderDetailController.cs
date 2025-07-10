using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;

namespace MultiShop.Order.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderDetailController(
    GetOrderDetailQueryHandler getOrderDetailQueryHandler,
    GetOrderDetailByIdQueryHandler getOrderDetailByIdQueryHandler,
    CreateOrderDetailCommandHandler createOrderDetailCommandHandler,
    UpdateOrderDetailCommandHandler updateOrderDetailCommandHandler,
    RemoveOrderDetailCommandHandler deleteOrderDetailCommandHandler) : ControllerBase
{
    private readonly GetOrderDetailQueryHandler _getOrderDetailQueryHandler = getOrderDetailQueryHandler;
    private readonly GetOrderDetailByIdQueryHandler _getOrderDetailByIdQueryHandler = getOrderDetailByIdQueryHandler;
    private readonly CreateOrderDetailCommandHandler _createOrderDetailCommandHandler = createOrderDetailCommandHandler;
    private readonly UpdateOrderDetailCommandHandler _updateOrderDetailCommandHandler = updateOrderDetailCommandHandler;
    private readonly RemoveOrderDetailCommandHandler _deleteOrderDetailCommandHandler = deleteOrderDetailCommandHandler;

    [HttpGet]
    public async Task<IActionResult> GetAllOrderDetails()
    {
        var result = await _getOrderDetailQueryHandler.Handle();
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderDetailById(int id)
    {
        var result = await _getOrderDetailByIdQueryHandler.Handle(new GetOrderDetailByIdQuery(id));
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrderDetail([FromBody] CreateOrderDetailCommand command)
    {
        if (command == null)
        {
            return BadRequest("Invalid order detail data.");
        }
        var result = await _createOrderDetailCommandHandler.Handle(command);
        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateOrderDetail([FromBody] UpdateOrderDetailCommand command)
    {
        if (command == null)
        {
            return BadRequest("Invalid order detail data.");
        }
        var result = await _updateOrderDetailCommandHandler.Handle(command);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderDetail(int id)
    {
        var result = await _deleteOrderDetailCommandHandler.Handle(new RemoveOrderDetailCommand(id));
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}
