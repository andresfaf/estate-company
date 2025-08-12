using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.DTOs;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Queries.Properties.GetAll
{
    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, List<PropertyDto>>
    {
        private readonly ILogger<GetAllPropertiesQueryHandler> _logger;
        private readonly IPropertyRepository _repository;
        private readonly IImageRepository _imageRepository;
        private readonly IPropertyImageRepository _propertyImageRepository;
        public GetAllPropertiesQueryHandler(ILogger<GetAllPropertiesQueryHandler> logger, IPropertyRepository repository, IImageRepository imageRepository, IPropertyImageRepository propertyImageRepository)
        {
            _logger = logger;
            _repository = repository;
            _imageRepository = imageRepository;
            _propertyImageRepository = propertyImageRepository;
        }

        public async Task<List<PropertyDto>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var properties = await _repository.GetAll(cancellationToken);
                var propertiesDto = properties.Adapt<List<PropertyDto>>();

                //uso de paralelimos para ejecutar tareas de recuperacion y conversion de imagenes al tiempo
                var tasks = propertiesDto.Select(async dto =>
                {
                    var propertyImages = await _propertyImageRepository.GetByPropertyIdEnabled(dto.Id, cancellationToken);
                    if (propertyImages is not null)
                    {
                        var image = await _imageRepository.DownloadConvertedBase64(propertyImages.File, cancellationToken);
                        dto.ImageEnabled = image;
                    }
                });

                await Task.WhenAll(tasks);

                return propertiesDto;
            }
            catch (DatabaseConnectionException ex)
            {
                _logger.LogError(ex, "Error");
                throw;
            }
        }
    }
}
