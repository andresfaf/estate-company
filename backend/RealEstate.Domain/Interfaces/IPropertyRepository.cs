using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces
{
    public interface IPropertyRepository
    {
        Task<List<Property>> GetAll(CancellationToken cancellationToken);
        Task Add(Property property, CancellationToken cancellationToken);
        Task<List<Property>> GetByFilters(string? name, string? address, decimal? minPrice, decimal? maxPrice, CancellationToken cancellationToken);
        Task Delete(string id, CancellationToken cancellationToken);
        Task<Property> GetById(string id, CancellationToken cancellationToken);
    }
}
