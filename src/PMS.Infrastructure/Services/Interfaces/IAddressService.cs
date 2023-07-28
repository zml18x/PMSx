using PMS.Infrastructure.Dto;

namespace PMS.Infrastructure.Services.Interfaces
{
    public interface IAddressService
    {
        public Task<AddressDto> GetByIdAsync(Guid id);
        public Task<AddressDetailsDto> GetDetailsByIdAsync(Guid id);
        public Task CreateAsync(string countryCode, string region, string city, string postalCode,
            string street, string buildingNumber);
        public Task UpdateAsync(Guid id, string? countryCode, string? city, string? postalCode,
            string? street, string? buildingNumber, string? region);
    }
}