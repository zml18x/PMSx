using PMS.Core.Exceptions;
using System.Text.RegularExpressions;

namespace PMS.Core.Entities
{
    public class Address : Entity
    {
        public string Country { get; protected set; }
        public string CountryCode { get; protected set; }
        public string Region { get; protected set; }
        public string City { get; protected set; }
        public string PostalCode { get; protected set;}
        public string Street { get; protected set; }
        public string BuildingNumber { get; protected set; }



        public Address(Guid id, string country, string region, string countryCode, string city, string postalCode,
            string street, string buildingNumber)
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
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentNullException(nameof(country), "Country cannot be null or whitespace");

            if (!Regex.IsMatch(country, @"^[A-Za-z\s\-]+$"))
                throw new ArgumentException(nameof(country), "The country must contain only letters, spaces, or dashes");

            Country = country;
        }

        private void SetCountryCode(string countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                throw new ArgumentNullException(nameof(countryCode), "CountryCode cannot be null or whitespace");

            if (countryCode.Length != 2)
                throw new ArgumentException(nameof(countryCode), "The country code must be 2 characters long");

            if (!Regex.IsMatch(countryCode, @"^[A-Za-z]+$"))
                throw new ArgumentException(nameof(countryCode), "The country code must contain only letters");

            CountryCode = countryCode.ToUpper();
        }

        private void SetRegion(string region)
        {
            if (string.IsNullOrWhiteSpace(region))
                throw new ArgumentNullException(nameof(region), "Region cannot be null or whitespace");

            if (!Regex.IsMatch(region, @"^[A-Za-z\s\-]+$"))
                throw new ArgumentException(nameof(region), "The region must contain only letters, spaces, or dashes");

            Region = region;
        }

        private void SetCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException(nameof(city), "City cannot be null or whitespace");

            if (!Regex.IsMatch(city, @"^[A-Za-z\s\-]+$"))
                throw new ArgumentException(nameof(city), "The city must contain only letters, spaces, or dashes");

            City = city;
        }

        private void SetPostalCode(string postalCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
                throw new ArgumentNullException(nameof(postalCode), "PostalCode cannot be null or whitespace");


            switch (CountryCode.ToUpper())
            {
                // European countries

                case "DE": // Germany
                    if (!Regex.IsMatch(postalCode, @"^\d{5}$"))
                        throw new ArgumentException(nameof(postalCode), "Invalid postal code format for Germany");
                    break;

                case "FR": // France
                    if (!Regex.IsMatch(postalCode, @"^\d{5}$"))
                        throw new ArgumentException(nameof(postalCode), "Invalid postal code format for France");
                    break;

                case "PL": // Poland
                    if (!Regex.IsMatch(postalCode, @"^\d{2}-\d{3}$"))
                        throw new ArgumentException(nameof(postalCode), "Invalid postal code format for Poland");
                    break;

                case "AT": // Austria
                    if (!Regex.IsMatch(postalCode, @"^\d{4}$"))
                        throw new ArgumentException(nameof(postalCode), "Invalid postal code format for Austria");
                    break;

                case "IT": // Italy
                    if (!Regex.IsMatch(postalCode, @"^\d{5}$"))
                        throw new ArgumentException(nameof(postalCode), "Invalid postal code format for Italy");
                    break;

                case "NO": // Norway
                    if (!Regex.IsMatch(postalCode, @"^\d{4}$"))
                        throw new ArgumentException(nameof(postalCode), "Invalid postal code format for Norway");
                    break;

                case "DK": // Denmark
                    if (!Regex.IsMatch(postalCode, @"^\d{4}$"))
                        throw new ArgumentException(nameof(postalCode), "Invalid postal code format for Denmark");
                    break;

                // North American countries

                case "US": // United States
                    if (!Regex.IsMatch(postalCode, @"^\d{5}(?:[-\s]\d{4})?$"))
                        throw new ArgumentException(nameof(postalCode), "Invalid postal code format for the United States");
                    break;

                case "CA": // Canada
                    if (!Regex.IsMatch(postalCode, @"^[A-Za-z]\d[A-Za-z]\s\d[A-Za-z]\d$"))
                        throw new ArgumentException(nameof(postalCode), "Invalid postal code format for Canada");
                    break;

                default:
                    throw new ArgumentException(nameof(postalCode), "Unsupported country code");
            }

            PostalCode = postalCode;
        }

        private void SetStreet(string street)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentNullException(nameof(street), "Street cannot be null or whitespace");

            if (!Regex.IsMatch(street, @"^[A-Za-z0-9\s\-]+$"))
                throw new ArgumentException(nameof(street), "The street must contain only letters, numbers, spaces, or dashes");

            Street = street;
        }

        private void SetBuildingNumber(string buildingNumber)
        {
            if (string.IsNullOrWhiteSpace(buildingNumber))
                throw new ArgumentNullException(nameof(buildingNumber), "BuildingNumber cannot be null or whitespace");

            if (!Regex.IsMatch(buildingNumber, @"^[A-Za-z0-9\s\-]+$"))
                throw new ArgumentException(nameof(buildingNumber), "The building number must contain only letters, numbers, spaces, or dashes");

            BuildingNumber = buildingNumber;
        }
    }
}