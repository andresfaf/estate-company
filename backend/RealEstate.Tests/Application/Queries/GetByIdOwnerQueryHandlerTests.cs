using Moq;
using RealEstate.Application.Queries.Owners.GetById;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Tests.Application.Queries
{
    public class GetByIdOwnerQueryHandlerTests
    {
        private Mock<IOwnerRepository> _ownerRepoMock;
        private Mock<IImageRepository> _imageRepoMock;
        private GetByIdOwnerQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _ownerRepoMock = new Mock<IOwnerRepository>();
            _imageRepoMock = new Mock<IImageRepository>();
            _handler = new GetByIdOwnerQueryHandler(_ownerRepoMock.Object, _imageRepoMock.Object);
        }

        /*
         Test simulando data de owner, que procesa la imagen correctamente, y retorno de owner con foto en base64
         */
        [Test]
        public async Task Handle_ReturnOwnerDto_WithConvertedPhoto()
        {
            var ownerId = "123";
            var originalPhotoPath = "photo.png";
            var convertedBase64 = "base64_encoded_photo";

            var ownerEntity = new Owner
            {
                Id = ownerId,
                Name = "Andres acosta",
                Photo = originalPhotoPath
            };

            _ownerRepoMock
                .Setup(r => r.GetById(ownerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(ownerEntity);

            _imageRepoMock
                .Setup(r => r.DownloadConvertedBase64(originalPhotoPath, It.IsAny<CancellationToken>()))
                .ReturnsAsync(convertedBase64);

            var query = new GetByIdOwnerQuery(ownerId);

            
            var result = await _handler.Handle(query, CancellationToken.None);

            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(ownerId));
            Assert.That(result.Name, Is.EqualTo("Andres acosta"));
            Assert.That(result.Photo, Is.EqualTo(convertedBase64));

            _ownerRepoMock.Verify(r => r.GetById(ownerId, It.IsAny<CancellationToken>()), Times.Once);
            _imageRepoMock.Verify(r => r.DownloadConvertedBase64(originalPhotoPath, It.IsAny<CancellationToken>()), Times.Once);
        }



    }
}
