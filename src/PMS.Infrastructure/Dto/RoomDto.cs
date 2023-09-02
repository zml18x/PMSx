namespace PMS.Infrastructure.Dto
{
    public class RoomDto
    {
        public Guid RoomId { get; set; }
        public Guid PropertyId { get; set; }
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public bool IsOccupied { get; set; }



        public RoomDto(Guid roomId, Guid propertyId, string roomNumber, string type, bool isOccupied)
        {
            RoomId = roomId;
            PropertyId = propertyId;
            RoomNumber = roomNumber;
            Type = type;
            IsOccupied = isOccupied;
        }
    }
}