using MediatR;

namespace RealEstate.Application.Commands.Owners.Create
{
    public record CreateOwnerCommand : IRequest<string>
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateOnly Birthday { get; set; }
        public Stream FileStream { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
    }
}
