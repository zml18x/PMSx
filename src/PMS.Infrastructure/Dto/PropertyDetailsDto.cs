namespace PMS.Infrastructure.Dto
{
    public class PropertyDetailsDto : PropertyDto
    {
        public Guid AddressId { get; set; }
        public IEnumerable<RoomDto> Rooms { get; set; }



        public PropertyDetailsDto(Guid id, string propertyType, int stars, string name, string description, int maxRoomsCount, int roomsCount, Guid addressId)
            : base(id, propertyType, stars, name, description, maxRoomsCount, roomsCount)
        {
            AddressId = addressId;
        }   
    }
}