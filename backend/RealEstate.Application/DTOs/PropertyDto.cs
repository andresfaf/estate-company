namespace RealEstate.Application.DTOs
{
    public class PropertyDto
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string IdOwner { get; set; } = string.Empty;
        public string ImageEnabled { get; set; } = string.Empty;
    }
}
