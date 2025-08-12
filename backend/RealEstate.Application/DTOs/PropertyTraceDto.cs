namespace RealEstate.Application.DTOs
{
    public class PropertyTraceDto
    {
        public string Id { get; set; } = string.Empty;
        public DateOnly DateSale { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal value { get; set; }
        public decimal Tax { get; set; }
        public string IdProperty { get; set; } = string.Empty;
    }
}
