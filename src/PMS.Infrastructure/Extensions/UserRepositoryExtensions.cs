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
                throw new NotFoundException($"User with ID '{userId}' does not exist");
            }

            return user;
        }

        public static async Task<UserProfile> GetOrFailAsync(this IUserProfileRepository repository, Guid userProfileId)
        {
            if (userProfileId == Guid.Empty)
            {
                throw new ArgumentNullException("The userProfileId parameter cannot be empty");
            }

            var userProfile = await repository.GetByIdAsync(userProfileId);

            if (userProfile == null)
            {
                throw new NotFoundException($"UserProfile with ID '{userProfileId}' does not exist");
            }

            return userProfile;
        }
    }
}