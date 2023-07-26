using Moq;
using Microsoft.Extensions.Configuration;
using PMS.Infrastructure.Services;
using PMS.Core.Entities;

namespace PMS.Infrastructure.UnitTests.Services
{
    public class JwtServiceTests
    {
        private Mock<IConfiguration> _configurationMock;
        private JwtService _jwtService;


        private void Setup()
        {
            _configurationMock = new Mock<IConfiguration>();
            _jwtService = new JwtService(_configurationMock.Object);
        }

        [Fact]
        public void CreateTokenThrowsArgumentNullExceptionWhenUserIsNull()
        {
            Setup();

            Assert.Throws<ArgumentNullException>(() => _jwtService.CreateToken(null!));
        }

        [Fact]
        public void CreateTokenValidUserReturnTokenDto()
        {
            Setup();

            var userId = Guid.NewGuid();
            var userProfileiD = Guid.NewGuid();
            var email = "test@mail.com";
            var passwordHash = new byte[64];
            var passwordSalt = new byte[128];

            var user = new User(userId,userProfileiD, email, passwordHash, passwordSalt);

            _configurationMock.Setup(c => c.GetSection("JWT:Key").Value).Returns("dsam;dals###11324");
            _configurationMock.Setup(c => c.GetSection("JWT:ExpiryMinutes").Value).Returns("60");
            _configurationMock.Setup(c => c.GetSection("JWT:Issuer").Value).Returns("https://localhost:7099");


            var token = _jwtService.CreateToken(user);

            Assert.NotNull(token);
            Assert.Equal(user.Role, token.Role);
            Assert.Equal(DateTime.UtcNow.AddMinutes(60).Minute, token.Expires.Minute);
        }
    }
}