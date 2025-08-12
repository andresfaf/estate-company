namespace RealEstate.API.Helpers
{
    public static class ImageHelper
    {
        public static async Task<string> ConverFileToBase64(IFormFile formFile, CancellationToken cancellationToken)
        {
            string? base64Image = null;

            using var ms = new MemoryStream();
            await formFile.CopyToAsync(ms, cancellationToken);
            var imageBytes = ms.ToArray();
            base64Image = Convert.ToBase64String(imageBytes);

            return $"data:{formFile.ContentType};base64,{base64Image}";
        }

        public static async Task<byte[]> ConvertIFileToBytes(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
