using PMS.Infrastructure.Dto;

namespace PMS.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> GetAsync(Guid userId);
        public Task<UserProfileDto> GetDetailsAsync(Guid userId);
        public Task RegisterAsync(string email, string password, string firstName, string lastName, string phoneNumber);
        public Task<JwtDto> LoginAsync(string email, string password);
        public Task UpdateProfileAsync(Guid id, string firstName, string lastName, string phoneNumber);
    }
}