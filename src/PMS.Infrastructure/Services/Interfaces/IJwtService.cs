using PMS.Infrastructure.Dto;

namespace PMS.Infrastructure.Services.Interfaces
{
    public interface IJwtService
    {
        public JwtDto CreateToken(Core.Entities.User user);
    }
}