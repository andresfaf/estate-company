using MediatR;
using RealEstate.Application.DTOs;

namespace RealEstate.Application.Queries.PropertyTraces.GetByPropertyId
{
    public record GetPropertyWithTracesQuery(string propertyId): IRequest<PropertyWithTracesDto>
    {
    }
}
