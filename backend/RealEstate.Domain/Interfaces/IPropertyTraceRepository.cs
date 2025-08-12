using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces
{
    public interface IPropertyTraceRepository
    {
        Task Add(PropertyTrace propertyTrace, CancellationToken cancellationToken);
        Task<List<PropertyTrace>> GetByPropertyId(string propertyId, CancellationToken cancellationToken);
        Task DeleteManyByPropertyId(string propertyId, CancellationToken cancellationToken);
    }
}
