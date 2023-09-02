using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Dto;
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
                propertiesDto.Add(new PropertyDto(property.Id, property.PropertyType, property.Stars, property.Name, property.Description));
            }

            return propertiesDto;
        }

        public async Task<PropertyDetailsDto> GetByIdAsync(Guid propertyId)
        {
            var property = await _propertyRepository.GetWithRoomsByPropertyId(propertyId);

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

        public async Task AddRoomsAsync(Guid propertyId, int amount, string[] roomNumber, string[] name,
            string[] description, string[] type, int[] singleBedCount, int[] doubleBedCount)
        {
            var property = await _propertyRepository.GetWithRoomsByPropertyId(propertyId);

            if (property == null)
                throw new ArgumentNullException(nameof(property), $"Property with ID '{propertyId}' does not exist");

            var availableSpace = property.MaxRoomsCount - property.RoomsCount;

            if (amount > availableSpace)
                throw new ArgumentException($"Only {availableSpace} rooms can be added", nameof(amount));

            var newRooms = new List<Room>();

            for(var i = 0; i < amount; i++)
            {
                var room = new Room(Guid.NewGuid(), propertyId, roomNumber[i], name[i], description[i], type[i], singleBedCount[i], doubleBedCount[i]);

                newRooms.Add(room);
            }

            await _propertyRepository.AddRoomsAsync(newRooms);
        }

        public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync(Guid propertyId)
        {
            var property = await _propertyRepository.GetWithRoomsByPropertyId(propertyId);

            if (property == null)
                throw new ArgumentNullException(nameof(property), $"Property with ID '{propertyId} does not exist'");

            var roomsDto = new List<RoomDto>();

            foreach(var room in property.Rooms)
                roomsDto.Add(new RoomDto(room.Id, room.PropertyId, room.RoomNumber, room.Type, room.IsOccupied));

            return roomsDto;
        }

        public async Task<RoomDetailsDto> GetRoomAsync(Guid propertyId, Guid roomId)
        {
            var room = await _propertyRepository.GetRoomAsync(propertyId, roomId);

            if (room == null)
                throw new ArgumentNullException(nameof(room), $"Room with PropertyId = '{propertyId}' and RoomId = '{roomId} does not exist'");

            return new RoomDetailsDto(room.Id, room.PropertyId, room.RoomNumber, room.Type, room.IsOccupied, room.Name, room.Description, room.SingleBedCount, room.DoubleBedCount);
        }
    }
}