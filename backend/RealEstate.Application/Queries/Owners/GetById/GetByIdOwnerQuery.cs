using MediatR;
using RealEstate.Application.DTOs;

namespace RealEstate.Application.Queries.Owners.GetById
{
    public record GetByIdOwnerQuery(string id) : IRequest<OwnerDto>
    {
    }
}
