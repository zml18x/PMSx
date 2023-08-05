using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Dto;
using PMS.Infrastructure.Extensions;
using PMS.Infrastructure.Services.Interfaces;

namespace PMS.Infrastructure.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;



        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }



        public async Task<IEnumerable<PropertyDto>> GetPropertiesAsync(Guid userId)
        {
            var properties = await _propertyRepository.GetAllAsync(userId);

            var propertiesDto = new HashSet<PropertyDto>();

            foreach (var property in properties)
            {
                propertiesDto.Add(new PropertyDto(property.Id, property.PropertyType, property.Stars, property.Name, property.Description,
                    property.MaxRoomsCount, property.RoomsCount));
            }

            return propertiesDto;
        }

        public async Task<PropertyDetailsDto> GetByIdAsync(Guid propertyId)
        {
            var property = await _propertyRepository.GetOrFailAsync(propertyId);

            return new PropertyDetailsDto(property.Id, property.PropertyType, property.Stars,
                property.Name, property.Description, property.MaxRoomsCount, property.RoomsCount, property.AddressId);
        }

        public async Task CreateAsync(Guid userId, Guid addressId, string propertyType, int stars, string name, string description, int maxRoomsCount)
        {
            var property = await _propertyRepository.GetByNameAsync(userId, name);

            if (property != null)
                throw new ArgumentException($"Property named '{name} already exist'");

            var propertyId = Guid.NewGuid();

            property = new Property(propertyId, userId, addressId, propertyType, stars, name, description, maxRoomsCount);

            await _propertyRepository.CreateAsync(property);
        }
    }
}