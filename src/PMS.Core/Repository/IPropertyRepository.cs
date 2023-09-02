using PMS.Core.Entities;

namespace PMS.Core.Repository
{
    public interface IPropertyRepository
    {
        public Task<Property> GetByIdAsync(Guid propertyId);
        public Task<Property> GetByNameAsync(Guid userId, string name);
        public Task<Property> GetWithRoomsByPropertyId(Guid propertyId);
        public Task<IEnumerable<Property>> GetAllAsync(Guid userId);
        public Task CreateAsync(Property property);
        public Task UpdateAsync(Property property);
        public Task DeleteAsync(Property property);
        public Task AddRoomsAsync(List<Room> rooms);
        public Task<Room> GetRoomAsync(Guid propertyId, Guid roomId);
    }
}