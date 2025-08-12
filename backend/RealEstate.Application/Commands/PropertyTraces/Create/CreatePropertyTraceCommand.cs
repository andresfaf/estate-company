using MediatR;

namespace RealEstate.Application.Commands.PropertyTraces.Create
{
    public record CreatePropertyTraceCommand : IRequest<string>
    {
        public DateOnly DateSale { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal value { get; set; }
        public decimal Tax { get; set; }
        public string IdProperty { get; set; } = string.Empty;
    }
}
