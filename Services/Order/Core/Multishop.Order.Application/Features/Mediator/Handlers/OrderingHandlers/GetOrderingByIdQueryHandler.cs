using MediatR;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers;
public class GetOrderingByIdQueryHandler(IRepository<Ordering> repository) : IRequestHandler<GetOrderingByIdQuery, GetOrderingByIdQueryResult>
{
    private readonly IRepository<Ordering> _repository = repository;
    
    public async Task<GetOrderingByIdQueryResult> Handle(GetOrderingByIdQuery request, CancellationToken cancellationToken)
    {
        var value = await _repository.GetByIdAsync(request.Id);
        if (value == null)
        {
            return null; // or throw an exception if you prefer
        }
        return new GetOrderingByIdQueryResult
        {
            OrderingId = value.OrderingId,
            UserId = value.UserId,
            TotalPrice = value.TotalPrice,
            OrderDate = value.OrderDate
        };
    }
}

