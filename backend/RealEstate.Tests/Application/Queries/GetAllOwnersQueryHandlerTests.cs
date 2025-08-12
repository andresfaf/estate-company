using Moq;
using RealEstate.Application.Queries.Owners.GetAll;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Tests.Queries
{
    [TestFixture]
    public class GetAllOwnersQueryHandlerTests
    {
        private Mock<IOwnerRepository> _ownerRepoMock;
        private Mock<IImageRepository> _imageRepoMock;
        private GetAllOwnersQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _ownerRepoMock = new Mock<IOwnerRepository>();
            _imageRepoMock = new Mock<IImageRepository>();

            _handler = new GetAllOwnersQueryHandler(
                _ownerRepoMock.Object,
                _imageRepoMock.Object
            );
        }

        /*
         Test simulando datos, conversion de imagen a base64, que entre al ciclo de conversion de imagenes las 
         veces que tiene que entrar 
         */
        [Test]
        public async Task Handle_ReturnOwnersWithBase64Photos()
        {
            var owners = new List<Owner>
            {
                new Owner { Id = "1", Name = "Andres", Photo = "photo1.jpg" },
                new Owner { Id = "2", Name = "Felipe", Photo = "photo2.jpg" }
            };

            _ownerRepoMock
                .Setup(r => r.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(owners);

            _imageRepoMock
                .Setup(r => r.DownloadConvertedBase64(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((string photo, CancellationToken _) => $"Base64_{photo}");

            var result = await _handler.Handle(new GetAllOwnersQuery(), CancellationToken.None);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.All(o => o.Photo.StartsWith("Base64_")), Is.True);

            _ownerRepoMock.Verify(r => r.GetAll(It.IsAny<CancellationToken>()), Times.Once);
            _imageRepoMock.Verify(r => r.DownloadConvertedBase64(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        /*
         Test simulando que la consulta de owners retorna una lista vacia, y verifica que no se llame al repositorio 
         que descarga y hace la conversion de imagen
         */
        [Test]
        public async Task Handle_ReturnEmptyList_WhenNoOwnersFound()
        {
            _ownerRepoMock
                .Setup(r => r.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Owner>());

            var result = await _handler.Handle(new GetAllOwnersQuery(), CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);

            _ownerRepoMock.Verify(r => r.GetAll(It.IsAny<CancellationToken>()), Times.Once);
            _imageRepoMock.Verify(
                r => r.DownloadConvertedBase64(It.IsAny<string>(), It.IsAny<CancellationToken>()),
                Times.Never
            );
        }
    }
}
