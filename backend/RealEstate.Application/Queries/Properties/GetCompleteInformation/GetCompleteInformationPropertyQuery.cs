using MediatR;
using RealEstate.Application.DTOs;

namespace RealEstate.Application.Queries.Properties.GetCompleteInformation
{
    public record GetCompleteInformationPropertyQuery(string propertyId) : IRequest<PropertyCompleteInformationDto>
    {
    }
}
