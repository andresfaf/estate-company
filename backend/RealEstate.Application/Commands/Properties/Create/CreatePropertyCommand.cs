using MediatR;
using RealEstate.Application.DTOs;

namespace RealEstate.Application.Commands.Properties.Create
{
    public record CreatePropertyCommand : IRequest<string>
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Year { get; set; } = string.Empty;
        public string IdOwner { get; set; } = string.Empty;
        public List<FileDataDto> FilesDataDto { get; set; }
    }
}
