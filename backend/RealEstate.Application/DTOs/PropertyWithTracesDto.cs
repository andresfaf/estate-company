namespace RealEstate.Application.DTOs
{
    public class PropertyWithTracesDto
    {
        public PropertyDto? Property { get; set; }
        public List<PropertyTraceDto>? PropertyTraces { get; set; }
    }
}
