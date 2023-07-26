using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Dto;
using PMS.Infrastructure.Extensions;
using PMS.Infrastructure.Services.Interfaces;

namespace PMS.Infrastructure.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;


        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }



        public async Task<AddressDto> GetByIdAsync(Guid id)
        {
            var address = await _addressRepository.GetAsync(id);

            return new AddressDto(address.Country, address.City, address.PostalCode, address.Street, address.BuildingNumber);
        }

        public async Task<AddressDetailsDto> GetDetailsByIdAsync(Guid id)
        {
            var address = await _addressRepository.GetAsync(id);

            return new AddressDetailsDto(address.Country, address.CountryCode, address.City,
                address.PostalCode, address.Street, address.BuildingNumber, address.Region);
        }

        public async Task CreateAsync(string country, string countryCode, string city,
            string postalCode, string street, string buildingNumber, string? region = null)
        {
            var addressId = Guid.NewGuid();

            var address = new Address(addressId, country, countryCode, city, postalCode, street, buildingNumber, region);

            await _addressRepository.CreateAsync(address);
        }

        public async Task UpdateAsync(Guid id, string? country, string? countryCode, string? city,
            string? postalCode, string? street, string? buildingNumber, string? region)
        {
            var address = await _addressRepository.GetOrFailAsync(id);

            address.UpdateAddress(country, countryCode, city, postalCode, street, buildingNumber, region);

            await _addressRepository.UpdateAsync(address);
        }
    }
}