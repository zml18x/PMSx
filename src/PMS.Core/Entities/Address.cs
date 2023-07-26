using PMS.Core.Exceptions;

namespace PMS.Core.Entities
{
    public class Address : Entity
    {
        public string Country { get; protected set; }
        public string CountryCode { get; protected set; }
        public string? Region { get; protected set; }
        public string City { get; protected set; }
        public string PostalCode { get; protected set;}
        public string Street { get; protected set; }
        public string BuildingNumber { get; protected set; }



        public Address(Guid id, string country, string countryCode, string city, string postalCode,
            string street, string buildingNumber, string? region = null)
        {
            SetId(id);
            SetCountry(country);
            SetCountryCode(countryCode);
            SetCity(city);
            SetPostalCode(postalCode);
            SetStreet(street);
            SetBuildingNumber(buildingNumber);
            SetRegion(region);
        }

     

        public override string ToString()
        {
            var addressLine = string.IsNullOrEmpty(BuildingNumber) ? Street : $"{Street} {BuildingNumber}";

            return $"{addressLine}, {PostalCode} {City}, {Country}";
        }

        public void UpdateAddress(string? country, string? countryCode, string? city, string? postalCode,
            string? street, string? buildingNumber, string? region)
        {
            var updated = false;


            if (!string.IsNullOrWhiteSpace(country))
            {
                SetCountry(country);
                updated = true;
            }

            if (!string.IsNullOrWhiteSpace(countryCode))
            {
                SetCountryCode(countryCode);
                updated = true;
            }

            if (!string.IsNullOrWhiteSpace(city))
            {
                SetCity(city);
                updated = true;
            }

            if (!string.IsNullOrWhiteSpace(postalCode))
            {
                SetPostalCode(postalCode);
                updated = true;
            }

            if (!string.IsNullOrWhiteSpace(street))
            {
                SetStreet(street);
                updated = true;
            }

            if (!string.IsNullOrWhiteSpace(buildingNumber))
            {
                SetBuildingNumber(buildingNumber);
                updated = true;
            }

            if (!string.IsNullOrWhiteSpace(region))
            {
                SetRegion(region);
                updated = true;
            }


            if (updated)
                LastUpdateAt = DateTime.UtcNow;
        }

        private void SetId(Guid addressId)
        {
            if (addressId == Guid.Empty)
                throw new EmptyIdException("AddressId cannot be empty");

            Id = addressId;
        }

        private void SetCountry(string country)
        {
            Country = country;
        }

        private void SetCountryCode(string countryCode)
        {
            CountryCode = countryCode;
        }

        private void SetRegion(string? region)
        {
            Region = region;
        }

        private void SetCity(string city)
        {
            City = city;
        }

        private void SetPostalCode(string postalCode)
        {
            PostalCode = postalCode;
        }

        private void SetStreet(string street)
        {
            Street = street;
        }

        private void SetBuildingNumber(string buildingNumber)
        {
            BuildingNumber = buildingNumber;
        }
    }
}