using MediatR;

namespace RealEstate.Application.Commands.Owners.Delete
{
    public record DeleteOwnerCommand(string Id) : IRequest;
}
