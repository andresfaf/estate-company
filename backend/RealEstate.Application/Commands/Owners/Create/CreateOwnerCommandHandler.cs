using Mapster;
using MediatR;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Commands.Owners.Create
{
    public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, string>
    {
        private readonly IOwnerRepository _repository;
        private readonly IImageRepository _imageRepository;
        public CreateOwnerCommandHandler(IOwnerRepository repository, IImageRepository imageRepository)
        {
            _repository = repository;
            _imageRepository = imageRepository;
        }

        public async Task<string> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //Sube imagen por medio de GridFs
                var imageId = await _imageRepository.Upload(request.FileStream, request.FileName, request.ContentType, cancellationToken);

                var owner = request.Adapt<Owner>();
                owner.Photo = imageId;
                await _repository.Add(owner, cancellationToken);

                return owner.Id;
            }
            catch (DatabaseConnectionException ex)
            {
                throw;
            }
        }
    }
}
