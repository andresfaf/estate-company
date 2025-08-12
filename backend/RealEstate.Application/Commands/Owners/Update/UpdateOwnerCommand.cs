using MediatR;

namespace RealEstate.Application.Commands.Owners.Update
{
    public record UpdateOwnerCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateOnly Birthday { get; set; }
        public Stream FileStream { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public bool SelectedFile { get; set; }
    }
}
