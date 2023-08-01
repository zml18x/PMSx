using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Exceptions;

namespace PMS.Infrastructure.Extensions
{
    public static class PropertyRepositoryExtensions
    {
        public static async Task<Property> GetOrFailAsync(this IPropertyRepository repository,Guid propertyId)
        {
            if (propertyId == Guid.Empty)
            {
                throw new ArgumentNullException("The propertyId parameter cannot be empty");
            }

            var property = await repository.GetByIdAsync(propertyId);

            if (property == null)
            {
                throw new NotFoundException($"Property with ID '{propertyId}' does not exist");
            }

            return property;
        }
    }
}