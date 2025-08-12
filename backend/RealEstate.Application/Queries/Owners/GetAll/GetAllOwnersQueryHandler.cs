using Mapster;
using MediatR;
using RealEstate.Application.DTOs;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Queries.Owners.GetAll
{
    public class GetAllOwnersQueryHandler : IRequestHandler<GetAllOwnersQuery, List<OwnerDto>>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IImageRepository _imageRepository;
        public GetAllOwnersQueryHandler(IOwnerRepository ownerRepository, IImageRepository imageRepository)
        {
            _ownerRepository = ownerRepository;
            _imageRepository = imageRepository;
        }

        public async Task<List<OwnerDto>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _ownerRepository.GetAll(cancellationToken);

                //Uso de paralelismo para setear el id de GridFs por el base64 para la ui
                var tasks = data.Select(async item =>
                {
                    item.Photo = await _imageRepository.DownloadConvertedBase64(item.Photo, cancellationToken);
                });

                await Task.WhenAll(tasks);

                return data.Adapt<List<OwnerDto>>();
            }
            catch (DatabaseConnectionException ex)
            {
                throw;
            }
        }
    }
}

