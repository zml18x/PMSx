using PMS.Infrastructure.Dto;

namespace PMS.Infrastructure.Services.Interfaces
{
    public interface IAddressService
    {
        public Task<AddressDto> GetByIdAsync(Guid id);
        public Task<AddressDetailsDto> GetDetailsByIdAsync(Guid id);
        public Task CreateAsync(string country, string countryCode, string city, string postalCode,
            string street, string buildingNumber, string? region = null);
        public Task UpdateAsync(Guid id, string? country, string? countryCode, string? city, string? postalCode,
            string? street, string? buildingNumber, string? region);
    }
}