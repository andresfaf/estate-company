using Mapster;
using MediatR;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Commands.PropertyTraces.Create
{
    public class CreatePropertyTraceCommandHandler : IRequestHandler<CreatePropertyTraceCommand, string>
    {
        private readonly IPropertyTraceRepository _propertyTraceRepository;
        public CreatePropertyTraceCommandHandler(IPropertyTraceRepository propertyTraceRepository)
        {
            _propertyTraceRepository = propertyTraceRepository;
        }
        public async Task<string> Handle(CreatePropertyTraceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var propertyTrace = request.Adapt<PropertyTrace>();
                await _propertyTraceRepository.Add(propertyTrace, cancellationToken);

                return propertyTrace.Id;
            }
            catch (DatabaseConnectionException ex)
            {
                throw;
            }
        }
    }
}
