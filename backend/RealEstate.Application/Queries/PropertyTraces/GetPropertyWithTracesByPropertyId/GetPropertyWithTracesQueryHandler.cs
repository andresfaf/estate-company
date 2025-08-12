using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.DTOs;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Queries.PropertyTraces.GetByPropertyId
{
    public class GetPropertyWithTracesQueryHandler : IRequestHandler<GetPropertyWithTracesQuery, PropertyWithTracesDto>
    {
        private readonly ILogger<GetPropertyWithTracesQueryHandler> _logger;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyTraceRepository _propertyTraceRepository;
        public GetPropertyWithTracesQueryHandler(ILogger<GetPropertyWithTracesQueryHandler> logger, IPropertyRepository propertyRepository, IPropertyTraceRepository propertyTraceRepository)
        {
            _logger = logger;
            _propertyRepository = propertyRepository;
            _propertyTraceRepository = propertyTraceRepository;
        }
        public async Task<PropertyWithTracesDto> Handle(GetPropertyWithTracesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var property = await _propertyRepository.GetById(request.propertyId, cancellationToken);
                var propertyTraces = await _propertyTraceRepository.GetByPropertyId(request.propertyId, cancellationToken);

                if (property is null)
                    throw new KeyNotFoundException(($"No se encontró este propiedad con id: {request.propertyId}"));

                return new PropertyWithTracesDto
                {
                    Property = property.Adapt<PropertyDto>(),
                    PropertyTraces = propertyTraces.Adapt<List<PropertyTraceDto>>()
                };
            }
            catch (DatabaseConnectionException ex)
            {
                _logger.LogError(ex, "Error");
                throw;
            }      
        }
    }
}
