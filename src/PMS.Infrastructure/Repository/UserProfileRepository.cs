using PMS.Core.Entities;
using PMS.Core.Repository;

namespace PMS.Infrastructure.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        public Task<UserProfile> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(UserProfile userProfile)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserProfile userProfile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(UserProfile userProfile)
        {
            throw new NotImplementedException();
        }    
    }
}