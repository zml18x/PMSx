using Microsoft.EntityFrameworkCore;
using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Data.Context;

namespace PMS.Infrastructure.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly PmsDbContext _context;



        public AddressRepository(PmsDbContext context)
        {
            _context = context;
        }



        public async Task<Address> GetAsync(Guid id)
            => await Task.FromResult(await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id));

        public async Task CreateAsync(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address), "Address cannot be null");
            }

            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address), "Address cannot be null");
            }

            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address), "Address cannot be null");
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
        }
    }
}