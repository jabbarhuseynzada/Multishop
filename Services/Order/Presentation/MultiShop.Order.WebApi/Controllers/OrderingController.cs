﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;

namespace MultiShop.Order.WebApi.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OrderingController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> OrderingList()
    {
        var value = await _mediator.Send(new GetOrderingQuery());
        return Ok(value);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderingById(int id)
    {
        var value = await _mediator.Send(new GetOrderingByIdQuery(id));
        if (value == null)
        {
            return NotFound();
        }
        return Ok(value);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrdering(CreateOrderingCommand command)
    {
        await _mediator.Send(command);
        return Ok("Created successfully");
    }
    [HttpPut]
    public async Task<IActionResult> UpdateOrdering(UpdateOrderingCommand command)
    {
        await _mediator.Send(command);
        return Ok("Updated successfully");
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveOrdering(int id)
    {
        await _mediator.Send(new RemoveOrderingCommand(id));
        return Ok("Deleted successfully");
    }
}
