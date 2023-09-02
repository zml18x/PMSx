using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Core.Entities
{
    public class Property : Entity
    {
        [NotMapped]
        private ISet<Room> _rooms = new HashSet<Room>();
        [NotMapped]
        private ISet<PropertyAdditionalServices> _additionalServices = new HashSet<PropertyAdditionalServices>();
        [Key]
        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public Guid AddressId { get; protected set; }
        public string PropertyType { get; protected set; }
        public int Stars { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int MaxRoomsCount { get; protected set; }

        [NotMapped]
        public int RoomsCount => _rooms.Count;
        [NotMapped]
        public IEnumerable<Room> Rooms => _rooms;
        [NotMapped]
        public IEnumerable<Room> AvailableRooms => _rooms.Where(r => !r.IsOccupied);
        [NotMapped]
        public IEnumerable<Room> OccupiedRooms =>_rooms.Except(AvailableRooms);
        [NotMapped]
        public IEnumerable<PropertyAdditionalServices> AdditionalServices => _additionalServices;



        public Property(Guid id, Guid userId, Guid addressId, string propertyType, int stars, string name, string description, int maxRoomsCount)
        {
            SetId(id, userId, addressId);
            SetPropertyType(propertyType);
            SetStars(stars);
            SetName(name);
            SetDescription(description);
            SetMaxRoomsCount(maxRoomsCount);
        }



        private void SetId(Guid propertyId, Guid userId, Guid addressId)
        {
            if (propertyId == Guid.Empty)
                throw new ArgumentNullException(nameof(propertyId), "PropertyId cannot be empty");

            if(userId == Guid.Empty)
                throw new ArgumentNullException(nameof(propertyId), "UserId cannot be empty");

            if (addressId == Guid.Empty)
                throw new ArgumentNullException(nameof(addressId), "AddressId cannot be empty");

            Id = propertyId;
            UserId = userId;
            AddressId = addressId;
        }

        private void SetPropertyType(string propertyType)
        {
            if (string.IsNullOrWhiteSpace(propertyType))
                throw new ArgumentNullException(nameof(propertyType), "PropertyType cannot be null or whitespace");

            PropertyType = propertyType;
        }

        private void SetStars(int stars)
        {
            if (stars < 0 || stars > 5)
                throw new ArgumentException("The number of stars should be between 0 and 5", nameof(stars));

            Stars = stars;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Name cannot be null or whitespace");

            Name = name;
        }

        private void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException(nameof(description), "Description cannot be null or whitespace");

            Description = description;
        }

        private void SetMaxRoomsCount(int maxRoomsCount)
        {
            if (maxRoomsCount < 0)
                throw new ArgumentException("The maximum number of rooms must be between 1 and 1000", nameof(maxRoomsCount));

            MaxRoomsCount = maxRoomsCount;
        }

        public void AddRoom(Room room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room), "The Room field cannot be empty");

            if (RoomsCount == MaxRoomsCount)
                throw new ArgumentException("You cannot add new rooms. The limit of the number of rooms has been reached.",nameof(room));

            _rooms.Add(room);
        }
    }
}