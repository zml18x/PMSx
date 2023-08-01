using PMS.Core.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Core.Entities
{
    public class Room : Entity
    {
        public Guid PropertyId { get; protected set; }
        public string RoomNumber { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Type { get; protected set; }       
        public int SingleBedCount { get; protected set; }
        public int DoubleBedCount { get; protected set; }
        public int MaxAccommodation => SingleBedCount + (DoubleBedCount * 2);
        public bool IsOccupied { get; protected set; }



        public Room(Guid id, Guid propertyId, string roomNumber, string name, string type, int singleBedCount, int doubleBedCount)
        {
            SetId(id, propertyId);
            SetNumber(roomNumber);
            SetName(name);
            SetType(type);
            SetBedsCount(singleBedCount, doubleBedCount);
            IsOccupied = false;
        }



        private void SetId(Guid roomId, Guid propertyId)
        {
            if (roomId == Guid.Empty)
                throw new EmptyIdException("RoomId cannot be empty");

            if (propertyId == Guid.Empty)
                throw new EmptyIdException("PropertyId cannot be empty");

            Id = roomId;
            PropertyId = propertyId;
        }

        private void SetNumber(string roomNumber)
        {
            if (string.IsNullOrWhiteSpace(roomNumber))
                throw new ArgumentNullException(nameof(roomNumber), "Room Number cannot be null or whitespace");

            RoomNumber = roomNumber;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Room Name cannot be null or whitespace");

            Name = name;
        }

        private void SetType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "Room Type cannot be null or whitespace");

            Type = type;
        }

        private void SetBedsCount(int singleBedCount, int doubleBedCount)
        {
            if (singleBedCount < 1 && doubleBedCount < 1)
                throw new ArgumentException("The room must contain at least one bed");

            if (singleBedCount < 0 || singleBedCount > 20)
                throw new ArgumentException("The number of singleBeds must be in the range of 0 to 20");

            if(doubleBedCount < 0 || doubleBedCount > 20)
                throw new ArgumentException("The number of doubleBedCount must be in the range of 0 to 20");

            SingleBedCount = singleBedCount;
            DoubleBedCount = doubleBedCount;
        }
    }
}