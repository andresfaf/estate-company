using Mapster;
using MediatR;
using RealEstate.Application.DTOs;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Queries.Properties.GetByFilters
{
    public class GetByFiltersPropertyQueryHandler : IRequestHandler<GetByFiltersPropertyQuery, List<PropertyDto>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IPropertyImageRepository _propertyImageRepository;
        public GetByFiltersPropertyQueryHandler(IPropertyRepository propertyRepository, IPropertyImageRepository propertyImageRepository, IImageRepository imageRepository)
        {
            _propertyRepository = propertyRepository;
            _propertyImageRepository = propertyImageRepository;
            _imageRepository = imageRepository;
        }

        public async Task<List<PropertyDto>> Handle(GetByFiltersPropertyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var properties = await _propertyRepository.GetByFilters(request.Name, request.Address, request.MinPrice, request.MaxPrice, cancellationToken);

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
                throw;
            }
        }
    }
}
