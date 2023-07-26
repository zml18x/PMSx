using PMS.Core.Entities;

namespace PMS.Core.Repository
{
    public interface IAddressRepository
    {
        public Task<Address> GetAsync(Guid id);
        public Task CreateAsync(Address address);
        public Task UpdateAsync(Address address);
        public Task DeleteAsync(Address address);
    }
}