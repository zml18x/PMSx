using System.ComponentModel.DataAnnotations;

namespace PMS.Infrastructure.Requests.Property
{
    public class CreateProperty
    {
        [Required]
        public string PropertyType { get; set; }
        [Required]
        public int Stars { get; set; }
        [Required]
        public string PropertyName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int MaxRoomsCount { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string BuildingNumber { get; set; }


        public CreateProperty(string propertyType, int stars, string propertyName, string description, int maxRoomsCount, string countryCode, string region, string city, string postalCode, string street, string buildingNumber)
        {
            PropertyType = propertyType;
            Stars = stars;
            PropertyName = propertyName;
            Description = description;
            MaxRoomsCount = maxRoomsCount;
            CountryCode = countryCode;
            Region = region;
            City = city;
            PostalCode = postalCode;
            Street = street;
            BuildingNumber = buildingNumber;
        }
    }
}