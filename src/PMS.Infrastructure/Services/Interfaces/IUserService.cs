using PMS.Infrastructure.Dto;

namespace PMS.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> GetAsync(Guid userId);
        public Task AddAsync(string email, string password, string firstName, string lastName, string phoneNumber);
        public Task LoginAsync(string email, string password);
    }
}