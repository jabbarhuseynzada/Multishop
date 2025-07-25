﻿using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
public class GetAddressQueryHandler(IRepository<Address> repository)
{
    private readonly IRepository<Address> _repository = repository;
    public async Task<List<GetAddressQueryResult>> Handle()
    {
        var values = await _repository.GetAllAsync();
        return values.Select(x => new GetAddressQueryResult
        {
            AddressId = x.AddressId,
            UserId = x.UserId,
            District = x.District,
            City = x.City,
            Detail = x.Detail
        }).ToList();
    }
}
