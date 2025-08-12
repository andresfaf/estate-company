namespace RealEstate.API.Dtos
{
    public class UpsertPropertyDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Year { get; set; } = string.Empty;
        public string IdOwner { get; set; } = string.Empty;
        public List<IFormFile> ImageFiles { get; set; }
        public List<bool>? ImageFilesEnabled { get; set; }
    }
}
