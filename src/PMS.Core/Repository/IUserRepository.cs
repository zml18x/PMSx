using PMS.Core.Entities;

namespace PMS.Core.Repository
{
    public interface IUserRepository
    {
        public Task<User> GetByIdAsync(Guid userId);
        public Task<User> GetByEmailAsync(string email);
        public Task CreateAsync(User user);
        public Task UpdateAsync(User user);
        public Task DeleteAsync(User user);
    }
}