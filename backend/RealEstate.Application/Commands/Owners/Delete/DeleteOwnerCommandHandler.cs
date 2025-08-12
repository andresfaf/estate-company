using MediatR;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Commands.Owners.Delete
{
    public class DeleteOwnerCommandHandler : IRequestHandler<DeleteOwnerCommand>
    {
        private readonly IOwnerRepository _repository;
        private readonly IImageRepository _imageRepository;

        public DeleteOwnerCommandHandler(IOwnerRepository repository, IImageRepository imageRepository)
        {
            _repository = repository;
            _imageRepository = imageRepository;
        }

        public async Task Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
        {
            //Elimina imagen de GridFs y dato de owner
            try
            {
                var owner = await _repository.GetById(request.Id, cancellationToken);

                if (owner == null)
                    throw new KeyNotFoundException($"No se encontró este owner con id: {request.Id}");

                await _imageRepository.Delete(owner.Photo, cancellationToken);
                await _repository.Delete(request.Id, cancellationToken);
            }
            catch (DatabaseConnectionException ex)
            {
                throw;
            }
        }
    }
}
