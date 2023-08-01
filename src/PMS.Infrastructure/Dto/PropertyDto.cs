namespace PMS.Infrastructure.Dto
{
    public class PropertyDto
    {
        public Guid Id { get; set; }
        public string PropertyType { get; set; }
        public int Stars { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxRoomsCount { get; set; }
        public int RoomsCount { get; set; }


        public PropertyDto(Guid id, string propertyType, int stars, string name, string description, int maxRoomsCount, int roomsCount)
        {
            Id = id;
            PropertyType = propertyType;
            Stars = stars;
            Name = name;
            Description = description;
            MaxRoomsCount = maxRoomsCount;
            RoomsCount = roomsCount;
        }
    }
}