using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Exceptions;

namespace PMS.Infrastructure.Extensions
{
    public static class UserRepositoryExtensions
    {
        public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid userId)
        {
            if(userId ==  Guid.Empty)
            {
                throw new ArgumentNullException("The userId parameter cannot be empty");
            }

            var user = await repository.GetByIdAsync(userId);

            if(user == null)
            {
                throw new UserNotFoundException($"User with ID '{userId}' does not exist");
            }

            return user;
        }
    }
}