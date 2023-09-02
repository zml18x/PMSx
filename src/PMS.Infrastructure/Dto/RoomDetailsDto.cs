namespace PMS.Infrastructure.Dto
{
    public class RoomDetailsDto : RoomDto
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int SingleBedCount { get; protected set; }
        public int DoubleBedCount { get; protected set; }
        public int MaxAccommodation => SingleBedCount + (DoubleBedCount * 2);


        public RoomDetailsDto(Guid roomId, Guid propertyId, string roomNumber, string type, bool isOccupied, string name, string description, int singleBedCount, int doubleBedCount) : base(roomId,propertyId, roomNumber, type, isOccupied)
        {
            Name = name;
            Description = description;
            SingleBedCount = singleBedCount;
            DoubleBedCount = doubleBedCount;
        }
    }
}