namespace PMS.Infrastructure.Dto
{
    public class AddressDetailsDto
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string? Region { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }



        public AddressDetailsDto(string country, string countryCode, string city,
            string postalCode, string street, string buildingNumber, string? region = null)
        {
            Country = country;
            CountryCode = countryCode;
            City = city;
            PostalCode = postalCode;
            Street = street;
            BuildingNumber = buildingNumber;
            Region = region;
        }
    }
}