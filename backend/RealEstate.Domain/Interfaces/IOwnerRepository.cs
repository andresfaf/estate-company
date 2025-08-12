using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces
{
    public interface IOwnerRepository
    {
        Task<List<Owner>> GetAll(CancellationToken cancellationToken);
        Task<Owner> GetById(string id, CancellationToken cancellationToken);
        Task Add(Owner owner, CancellationToken cancellationToken);
        Task<bool> Update(Owner owner, CancellationToken cancellationToken);
        Task Delete(string id, CancellationToken cancellationToken);
    }
}
