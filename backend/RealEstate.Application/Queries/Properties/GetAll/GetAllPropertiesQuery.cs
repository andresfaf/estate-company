using MediatR;
using RealEstate.Application.DTOs;

namespace RealEstate.Application.Queries.Properties.GetAll
{
    public record GetAllPropertiesQuery : IRequest<List<PropertyDto>>
    {
    }
}
