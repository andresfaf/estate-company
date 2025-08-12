namespace RealEstate.Application.DTOs
{
    public class FileDataDto
    {
        public Stream FileStream { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public bool? Enabled { get; set; }
    }
}
