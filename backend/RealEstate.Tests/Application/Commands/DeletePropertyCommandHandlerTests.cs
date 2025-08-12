using Moq;
using RealEstate.Application.Commands.Properties.Delete;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Tests.Commands
{
    [TestFixture]
    public class DeletePropertyCommandHandlerTests
    {
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        private Mock<IPropertyImageRepository> _propertyImageRepositoryMock;
        private Mock<IPropertyTraceRepository> _propertyTraceRepositoryMock;
        private Mock<IImageRepository> _imageRepositoryMock;
        private DeletePropertyCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _propertyImageRepositoryMock = new Mock<IPropertyImageRepository>();
            _propertyTraceRepositoryMock = new Mock<IPropertyTraceRepository>();
            _imageRepositoryMock = new Mock<IImageRepository>();

            _handler = new DeletePropertyCommandHandler(
                _propertyRepositoryMock.Object,
                _propertyImageRepositoryMock.Object,
                _propertyTraceRepositoryMock.Object,
                _imageRepositoryMock.Object
            );
        }

        /*
         Test para verificar eliminación exitosa de propiedad, validar que se eliminar 
         todas las imagenes correspondientes a esa propiedad incluyendo repositorio 
         simulado de GRIDfs
         */
        [Test]
        public async Task Handle_ShouldDeleteProperty_WhenPropertyExists()
        {
            var propertyId = "property 123";
            var property = new Property { Id = propertyId };
            var propertyImages = new List<PropertyImage>
            {
                new PropertyImage { File = "file1" },
                new PropertyImage { File = "file2" }
            };

            _propertyRepositoryMock
                .Setup(r => r.GetById(propertyId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(property);

            _propertyImageRepositoryMock
                .Setup(r => r.GetAllByPropertyId(propertyId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(propertyImages);

            _imageRepositoryMock
                .Setup(r => r.Delete(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _propertyImageRepositoryMock
                .Setup(r => r.DeleteManyByPropertyId(propertyId, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _propertyTraceRepositoryMock
                .Setup(r => r.DeleteManyByPropertyId(propertyId, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _propertyRepositoryMock
                .Setup(r => r.Delete(propertyId, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var command = new DeletePropertyCommand(propertyId);

            await _handler.Handle(command, CancellationToken.None);

            foreach (var img in propertyImages)
            {
                _imageRepositoryMock.Verify(r => r.Delete(img.File, It.IsAny<CancellationToken>()), Times.Once);
            }

            _propertyImageRepositoryMock.Verify(r => r.DeleteManyByPropertyId(propertyId, It.IsAny<CancellationToken>()), Times.Once);
            _propertyTraceRepositoryMock.Verify(r => r.DeleteManyByPropertyId(propertyId, It.IsAny<CancellationToken>()), Times.Once);
            _propertyRepositoryMock.Verify(r => r.Delete(propertyId, It.IsAny<CancellationToken>()), Times.Once);
        }

        /*
         Test para lanzar exepcion cuando no existe el dato
         */
        [Test]
        public void Handle_ShouldThrowKeyNotFoundException_WhenPropertyDoesNotExist()
        {
            var propertyId = "null property";

            _propertyRepositoryMock
                .Setup(r => r.GetById(propertyId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Property)null);

            var command = new DeletePropertyCommand(propertyId);

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.That(ex.Message, Does.Contain(propertyId));

            _imageRepositoryMock.Verify(r => r.Delete(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
            _propertyImageRepositoryMock.Verify(r => r.DeleteManyByPropertyId(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
            _propertyTraceRepositoryMock.Verify(r => r.DeleteManyByPropertyId(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
            _propertyRepositoryMock.Verify(r => r.Delete(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
