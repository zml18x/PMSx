using PMS.Core.Entities;
using PMS.Core.Repository;

namespace PMS.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetAsyncById(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsyncByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}