using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces
{
    public interface IPropertyImageRepository
    {
        Task AddMany(List<PropertyImage> propertyImages, CancellationToken cancellationToken);
        Task<PropertyImage> GetByPropertyIdEnabled(string id, CancellationToken cancellationToken);
        Task DeleteManyByPropertyId(string propertyId, CancellationToken cancellationToken);
        Task<List<PropertyImage>> GetAllByPropertyId(string propertyId, CancellationToken cancellationToken);
    }
}
