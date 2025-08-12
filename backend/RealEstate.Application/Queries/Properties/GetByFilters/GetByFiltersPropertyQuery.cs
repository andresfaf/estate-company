using MediatR;
using RealEstate.Application.DTOs;

namespace RealEstate.Application.Queries.Properties.GetByFilters
{
    public record GetByFiltersPropertyQuery(string? Name, string? Address, decimal? MinPrice, decimal? MaxPrice) : IRequest<List<PropertyDto>>
    {
    }
}
