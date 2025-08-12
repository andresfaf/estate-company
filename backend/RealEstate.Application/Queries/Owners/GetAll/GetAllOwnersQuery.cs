using MediatR;
using RealEstate.Application.DTOs;

namespace RealEstate.Application.Queries.Owners.GetAll
{
    public record GetAllOwnersQuery : IRequest<List<OwnerDto>>
    {
    }
}
