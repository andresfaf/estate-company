namespace RealEstate.Application.DTOs
{
    public class OwnerDto
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public DateOnly Birthday { get; set; }
    }
}
