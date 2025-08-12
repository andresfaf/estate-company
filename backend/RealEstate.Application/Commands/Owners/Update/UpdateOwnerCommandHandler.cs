using Mapster;
using MediatR;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Commands.Owners.Update
{
    public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand, bool>
    {
        private readonly IOwnerRepository _repository;
        private readonly IImageRepository _imageRepository;

        public UpdateOwnerCommandHandler(IOwnerRepository repository, IImageRepository imageRepository)
        {
            _repository = repository;
            _imageRepository = imageRepository;
        }

        public async Task<bool> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var owner = await _repository.GetById(request.Id, cancellationToken);

                if (owner == null)
                    throw new KeyNotFoundException($"No se encontró este owner con id: {request.Id}");

                if (request.SelectedFile)
                {
                    await _imageRepository.Delete(owner.Photo, cancellationToken);
                    var imageId = await _imageRepository.Upload(request.FileStream, request.FileName, request.ContentType, cancellationToken);
                    request.Photo = imageId;
                }
                else
                {
                    request.Photo = owner.Photo;
                }

                var newOwner = request.Adapt<Owner>();
                return await _repository.Update(newOwner, cancellationToken);
            }
            catch (DatabaseConnectionException ex)
            {
                throw;
            }
        }
    }
}
