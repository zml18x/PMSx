namespace PMS.Core.Entities
{
    public class Property : Entity
    {
        private ISet<Room> _rooms = new HashSet<Room>();
        public Guid AddressId { get; protected set; }
        public string PropertyType { get; protected set; }
        public int Stars { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int MaxRoomsCount { get; protected set; }
        public int RoomsCount => _rooms.Count;
        public IEnumerable<Room> Rooms => _rooms;
        public IEnumerable<Room> AvailableRooms => _rooms.Where(r => r.IsAvailable);
        public IEnumerable<Room> OccupiedRooms =>_rooms.Except(AvailableRooms);
        public AdditionalServices _AdditionalServices { get; protected set; }



        public Property(Guid id, Guid addressId, string propertyType, int stars, string name, string description, int maxRoomsCount, AdditionalServices additionalServices)
        {
            Id = id;
            AddressId = addressId;
            PropertyType = propertyType;
            Stars = stars;
            Name = name;
            Description = description;
            MaxRoomsCount = maxRoomsCount;
            _AdditionalServices = additionalServices;
        }
    }
}