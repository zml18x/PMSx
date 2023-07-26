using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Exceptions;

namespace PMS.Infrastructure.Extensions
{
    public static class AddressRepositoryExtensions
    {
        public static async Task<Address> GetOrFailAsync(this IAddressRepository repository, Guid addressId)
        {
            if (addressId == Guid.Empty)
            {
                throw new ArgumentNullException("The addressId parameter cannot be empty");
            }

            var address = await repository.GetAsync(addressId);

            if (address == null)
            {
                throw new NotFoundException($"Address with ID '{addressId}' does not exist");
            }

            return address;
        }
    }
}