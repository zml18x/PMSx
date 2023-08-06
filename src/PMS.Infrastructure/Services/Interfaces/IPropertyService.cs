using PMS.Infrastructure.Dto;

namespace PMS.Infrastructure.Services.Interfaces
{
    public interface IPropertyService
    {
        public Task<IEnumerable<PropertyDto>> GetPropertiesAsync(Guid userId);
        public Task<PropertyDetailsDto> GetByIdAsync(Guid propertyId);
        public Task CreateAsync(Guid userId, Guid addressId, string propertyType, int stars, string name, string description, int maxRoomsCount);
        public Task AddRoomsAsync(Guid propertyId, int amount, string[] roomNumber, string[] name,
            string[] description, string[] type, int[] singleBedCount, int[] doubleBedCount);
    }
}