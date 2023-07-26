namespace PMS.Infrastructure.Dto
{
    public class AddressDto
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }



        public AddressDto(string country, string city, string postalCode, string street, string buildingNumber)
        {
            Country = country;
            City = city;
            PostalCode = postalCode;
            Street = street;
            BuildingNumber = buildingNumber;
        }
    }
}