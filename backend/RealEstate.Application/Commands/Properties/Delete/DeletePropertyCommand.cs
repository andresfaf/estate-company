using MediatR;

namespace RealEstate.Application.Commands.Properties.Delete
{
    public record DeletePropertyCommand(string Id) : IRequest;

}
