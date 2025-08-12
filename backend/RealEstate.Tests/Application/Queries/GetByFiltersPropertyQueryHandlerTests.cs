using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using RealEstate.Application.Queries.Properties.GetByFilters;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

[TestFixture]
public class GetByFiltersPropertyQueryHandlerTests
{
    private Mock<IPropertyRepository> _propertyRepositoryMock;
    private Mock<IPropertyImageRepository> _propertyImageRepositoryMock;
    private Mock<IImageRepository> _imageRepositoryMock;
    private GetByFiltersPropertyQueryHandler _handler;

    [SetUp]
    public void Setup()
    {
        _propertyRepositoryMock = new Mock<IPropertyRepository>();
        _propertyImageRepositoryMock = new Mock<IPropertyImageRepository>();
        _imageRepositoryMock = new Mock<IImageRepository>();

        _handler = new GetByFiltersPropertyQueryHandler(
            _propertyRepositoryMock.Object,
            _propertyImageRepositoryMock.Object,
            _imageRepositoryMock.Object
        );
    }

    /*
      Test simulando envio de filtros, que se aplican, mapeo correcto de info
     */
    [Test]
    public async Task Handle_ShouldReturnFilteredProperties_WithImageEnabled()
    {
        var query = new GetByFiltersPropertyQuery("Casa de campo", "Calle 123", 100000, 1000000);

        var properties = new List<Property>
        {
            new Property { Id = "1", Name = "Casa de campo", Address = "Calle 123", Price = 2000 }
        };

        var propertyImage = new PropertyImage { File = "image-file" };
        var base64Image = "base64string";

        _propertyRepositoryMock
            .Setup(r => r.GetByFilters(query.Name, query.Address, query.MinPrice, query.MaxPrice, It.IsAny<CancellationToken>()))
            .ReturnsAsync(properties);

        _propertyImageRepositoryMock
            .Setup(r => r.GetByPropertyIdEnabled("1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(propertyImage);

        _imageRepositoryMock
            .Setup(r => r.DownloadConvertedBase64("image-file", It.IsAny<CancellationToken>()))
            .ReturnsAsync(base64Image);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].ImageEnabled, Is.EqualTo(base64Image));

        _propertyRepositoryMock.Verify(r => r.GetByFilters(query.Name, query.Address, query.MinPrice, query.MaxPrice, It.IsAny<CancellationToken>()), Times.Once);
        _propertyImageRepositoryMock.Verify(r => r.GetByPropertyIdEnabled("1", It.IsAny<CancellationToken>()), Times.Once);
        _imageRepositoryMock.Verify(r => r.DownloadConvertedBase64("image-file", It.IsAny<CancellationToken>()), Times.Once);
    }
}
