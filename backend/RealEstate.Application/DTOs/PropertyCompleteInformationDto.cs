namespace RealEstate.Application.DTOs
{
    public class PropertyCompleteInformationDto
    {
        public OwnerDto Owner { get; set; }
        public PropertyDto Property { get; set; }
        public List<PropertyImagesDto> PropertyImages { get; set; }
        public List<PropertyTraceDto> PropertyTraces { get; set; }
    }
}
