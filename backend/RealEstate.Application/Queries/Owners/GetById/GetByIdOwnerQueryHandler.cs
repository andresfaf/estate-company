using Mapster;
using MediatR;
using RealEstate.Application.DTOs;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Queries.Owners.GetById
{
    public class GetByIdOwnerQueryHandler : IRequestHandler<GetByIdOwnerQuery, OwnerDto>
    {
        private readonly IOwnerRepository _repository;
        private readonly IImageRepository _imageRepository;
        public GetByIdOwnerQueryHandler(IOwnerRepository repository, IImageRepository imageRepository)
        {
            _repository = repository;
            _imageRepository = imageRepository;
        }
        public async Task<OwnerDto> Handle(GetByIdOwnerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var owner = await _repository.GetById(request.id, cancellationToken);
                owner.Photo = await _imageRepository.DownloadConvertedBase64(owner.Photo, cancellationToken);
                return owner.Adapt<OwnerDto>();
            }
            catch (DatabaseConnectionException ex)
            {
                throw;
            }
        }
    }
}
