using Moq;
using RealEstate.Application.Commands.Owners.Create;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Tests.Application.Commands.Owners
{
    [TestFixture]
    public class CreateOwnerCommandHandlerTests
    {
        private Mock<IOwnerRepository> _ownerRepositoryMock;
        private Mock<IImageRepository> _imageRepositoryMock;
        private CreateOwnerCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _ownerRepositoryMock = new Mock<IOwnerRepository>();
            _imageRepositoryMock = new Mock<IImageRepository>();

            _handler = new CreateOwnerCommandHandler(
                _ownerRepositoryMock.Object,
                _imageRepositoryMock.Object
            );
        }

        /*
         Test para simular datos como filestrean, retorno de id, que se llaman los metodos de subir imagen y agregar owner 1 sola vez
         */
        [Test]
        public async Task Handle_UploadImage_AndAddOwner()
        {
            var fileStream = new MemoryStream(new byte[] { 1, 2, 3 });
            var fileName = "photo.jpg";
            var contentType = "image/jpeg";
            var expectedImageId = "img-123";
            var expectedOwnerId = Guid.NewGuid().ToString();

            _imageRepositoryMock
                .Setup(repo => repo.Upload(fileStream, fileName, contentType, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedImageId);

            _ownerRepositoryMock
                .Setup(repo => repo.Add(It.IsAny<Owner>(), It.IsAny<CancellationToken>()))
                .Callback<Owner, CancellationToken>((owner, _) => owner.Id = expectedOwnerId)
                .Returns(Task.CompletedTask);

            var command = new CreateOwnerCommand
            {
                FileStream = fileStream,
                FileName = fileName,
                ContentType = contentType,
                Name = "Andres acosta",
                Address = "Calle 123"
            };

            
            var resultId = await _handler.Handle(command, CancellationToken.None);

            Assert.That(resultId, Is.EqualTo(expectedOwnerId)); 
            _imageRepositoryMock.Verify(repo => repo.Upload(fileStream, fileName, contentType, It.IsAny<CancellationToken>()), Times.Once);
            _ownerRepositoryMock.Verify(repo => repo.Add(It.Is<Owner>(o => o.Photo == expectedImageId && o.Name == "Andres acosta"), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
