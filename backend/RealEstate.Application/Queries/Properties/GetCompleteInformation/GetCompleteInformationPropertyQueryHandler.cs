using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.DTOs;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Queries.Properties.GetCompleteInformation
{
    public class GetCompleteInformationPropertyQueryHandler : IRequestHandler<GetCompleteInformationPropertyQuery, PropertyCompleteInformationDto>
    {
        private readonly ILogger<GetCompleteInformationPropertyQueryHandler> _logger;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IPropertyTraceRepository _propertyTraceRepository;
        public GetCompleteInformationPropertyQueryHandler(ILogger<GetCompleteInformationPropertyQueryHandler> logger, IPropertyRepository propertyRepository, IOwnerRepository ownerRepository,
            IImageRepository imageRepository, IPropertyImageRepository propertyImageRepository, IPropertyTraceRepository propertyTraceRepository)
        {
            _logger = logger;
            _propertyRepository = propertyRepository;
            _ownerRepository = ownerRepository;
            _imageRepository = imageRepository;
            _propertyImageRepository = propertyImageRepository;
            _propertyTraceRepository = propertyTraceRepository;
        }

        public async Task<PropertyCompleteInformationDto> Handle(GetCompleteInformationPropertyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var property = await _propertyRepository.GetById(request.propertyId, cancellationToken);
                var owner = await _ownerRepository.GetById(property.IdOwner, cancellationToken);
                var propertyImages = await _propertyImageRepository.GetAllByPropertyId(property.Id, cancellationToken);
                var propertyTraces = await _propertyTraceRepository.GetByPropertyId(property.Id, cancellationToken);

                owner.Photo = await _imageRepository.DownloadConvertedBase64(owner.Photo, cancellationToken);
                foreach (var item in propertyImages)
                {
                    item.File = await _imageRepository.DownloadConvertedBase64(item.File, cancellationToken);
                }

                var response = new PropertyCompleteInformationDto
                {
                    Property = property.Adapt<PropertyDto>(),
                    Owner = owner.Adapt<OwnerDto>(),
                    PropertyImages = propertyImages.Adapt<List<PropertyImagesDto>>(),
                    PropertyTraces = propertyTraces.Adapt<List<PropertyTraceDto>>()
                };

                return response;
            }
            catch (DatabaseConnectionException ex)
            {
                _logger.LogError(ex, "Error");
                throw;
            }
        }
    }
}
