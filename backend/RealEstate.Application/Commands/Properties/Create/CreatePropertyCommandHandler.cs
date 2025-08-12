using Mapster;
using MediatR;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace RealEstate.Application.Commands.Properties.Create
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, string>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IImageRepository _imageRepository;
        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository, IPropertyImageRepository propertyImageRepository, IImageRepository imageRepository)
        {
            _propertyRepository = propertyRepository;
            _propertyImageRepository = propertyImageRepository;
            _imageRepository = imageRepository;
        }

        public async Task<string> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var property = request.Adapt<Property>();
                await _propertyRepository.Add(property, cancellationToken);

                var propertyImages = new List<PropertyImage>();
                //Sube las imagenes de la propiedad por medio GridFS y almacena los ids
                foreach (var item in request.FilesDataDto)
                {
                    using (item.FileStream)
                    {
                        var imageId = await _imageRepository.Upload(item.FileStream, item.FileName, item.ContentType, cancellationToken);
                        propertyImages.Add(new PropertyImage
                        {
                            File = imageId,
                            Enabled = (bool)item.Enabled,
                            IdProperty = property.Id
                        });
                    }
                }

                await _propertyImageRepository.AddMany(propertyImages, cancellationToken);
                return property.Id;
            }
            catch (DatabaseConnectionException ex)
            {
                throw;
            }
        }
    }
}
