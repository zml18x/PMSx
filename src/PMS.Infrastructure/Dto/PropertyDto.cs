namespace PMS.Infrastructure.Dto
{
    public class PropertyDto
    {
        public Guid Id { get; set; }
        public string PropertyType { get; set; }
        public int Stars { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public PropertyDto(Guid id, string propertyType, int stars, string name, string description)
        {
            Id = id;
            PropertyType = propertyType;
            Stars = stars;
            Name = name;
            Description = description;
        }
    }
}