using MediatR;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Commands.Properties.Delete
{
    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IPropertyTraceRepository _propertyTraceRepository;
        private readonly IImageRepository _imageRepository;

        public DeletePropertyCommandHandler(IPropertyRepository propertyRepository, IPropertyImageRepository propertyImageRepository,
           IPropertyTraceRepository propertyTraceRepository, IImageRepository imageRepository)
        {
            _propertyRepository = propertyRepository;
            _propertyImageRepository = propertyImageRepository;
            _propertyTraceRepository = propertyTraceRepository;
            _imageRepository = imageRepository;
        }

        public async Task Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            //Se elimina toda la info correspondiente a la propiedad, images, traces, GridFS
            try
            {
                var property = await _propertyRepository.GetById(request.Id, cancellationToken);

                if (property == null)
                    throw new KeyNotFoundException($"No se encontró propiedad con id: {request.Id}");

                var propertyImages = await _propertyImageRepository.GetAllByPropertyId(request.Id, cancellationToken);

                foreach (var item in propertyImages)
                {
                    await _imageRepository.Delete(item.File, cancellationToken);
                }

                //paralelismo para ejecutar tareas al mismo tiempo y reducir tiempo de ejecución
                await Task.WhenAll(
                    _propertyImageRepository.DeleteManyByPropertyId(request.Id, cancellationToken),
                    _propertyTraceRepository.DeleteManyByPropertyId(request.Id, cancellationToken)
                );

                await _propertyRepository.Delete(request.Id, cancellationToken);
            }
            catch (DatabaseConnectionException ex)
            {
                throw;
            }
           
        }
    }
}
