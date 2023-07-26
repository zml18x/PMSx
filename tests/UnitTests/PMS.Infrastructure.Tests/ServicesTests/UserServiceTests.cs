using Moq;
using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Dto;
using PMS.Infrastructure.Exceptions;
using PMS.Infrastructure.Services.Interfaces;
using PMS.Infrastructure.Services;
using System.Security.Authentication;
using System.Security.Cryptography;

namespace PMS.Infrastructure.UnitTests.Services
{
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IUserProfileRepository> _userProfileRepositoryMock;
        private Mock<IJwtService> _jwtServiceMock;
        private UserService _userService;


        private void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            _jwtServiceMock = new Mock<IJwtService>();
            _userService = new UserService(_userRepositoryMock.Object, _userProfileRepositoryMock.Object, _jwtServiceMock.Object);
        }

        [Fact]
        public async Task RegisterAsyncShouldInvokeAddAsyncOnUserRepository()
        {
            Setup();

            await _userService.RegisterAsync("test@mail.com", "passW0!RDxO", "Test", "Test", "123456789");

            _userRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task LoginAsyncShouldInvokeCreateTokenOnJwtServiceAndReturnTokenDto()
        {
            var userId = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var password = "pa$Sw0RD";
            var email = "test@mail.com";
            byte[] passwordHash;
            byte[] passwordSalt;

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            var user = new User(userId, userProfileId, email, passwordHash, passwordSalt);
            var token = new JwtDto(Guid.NewGuid().ToString(), DateTime.Now.AddDays(1), user.Role);

            Setup();

            _userRepositoryMock.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(user);
            _jwtServiceMock.Setup(x => x.CreateToken(user)).Returns(token);


            var result = await _userService.LoginAsync(email, password);


            _jwtServiceMock.Verify(x => x.CreateToken(user), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(token.Token, result.Token);
            Assert.Equal(token.Expires, result.Expires);
            Assert.Equal(token.Role, result.Role);
        }

        [Fact]
        public async Task LoginAsyncShouldThrowInvalidCredentialsExceptionWhenUserNotFound()
        {
            Setup();

            var email = "test@mail.com";
            var password = "pa$SW0rDD!X";

            _userRepositoryMock.Setup(x => x.GetByEmailAsync(email))!.ReturnsAsync((User)null);

            await Assert.ThrowsAsync<InvalidCredentialException>(() => _userService.LoginAsync(email, password));
        }

        [Fact]
        public async Task LoginAsyncShouldThrowInvalidCredentialsExceptionPasswordIncorrect()
        {
            Setup();

            var userId = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var password = "pa$Sw0RD";
            var email = "test@mail.com";
            byte[] passwordHash;
            byte[] passwordSalt;

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            var user = new User(userId, userProfileId, email, passwordHash, passwordSalt);

            _userRepositoryMock.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(user);

            await Assert.ThrowsAsync<InvalidCredentialException>(() => _userService.LoginAsync(user.Email, "incorrectPassword"));
        }

        [Fact]
        public async Task GetAsyncShouldReturnUserDtoIfIdIsCorrect()
        {
            Setup();

            var userId = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var email = "test@mail.com";
            byte[] passwordHash = new byte[64];
            byte[] passwordSalt = new byte[128];

            var user = new User(userId, userProfileId, email, passwordHash, passwordSalt);

            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);

            var result = await _userService.GetAsync(user.Id);

            _userRepositoryMock.Verify(x => x.GetByIdAsync(user.Id), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(email, result.Email);
            Assert.Equal(userProfileId, result.UserProfileId);
        }

        [Fact]
        public async Task GetAsyncThrowsUserNotFoundExceptionWhenUserWithIdDoesNotExist()
        {
            Setup();

            var id = Guid.NewGuid();

            await Assert.ThrowsAsync<NotFoundException>(() => _userService.GetAsync(id));
        }

        [Fact]
        public async Task GetDetailsAsyncShouldReturnUserProfileDtoIfIdIsCorrect()
        {
            Setup();

            var userId = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var email = "test@mail.com";
            byte[] passwordHash = new byte[64];
            byte[] passwordSalt = new byte[128];
            var firstName = "Test";
            var lastName = "LastNameTest";
            var phoneNumber = "123456789";

            var user = new User(userId, userProfileId, email, passwordHash, passwordSalt);
            var userProfile = new UserProfile(userProfileId, firstName, lastName, phoneNumber);

            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);
            _userProfileRepositoryMock.Setup(x => x.GetByIdAsync(userProfileId)).ReturnsAsync(userProfile);

            var result = await _userService.GetDetailsAsync(user.Id);

            _userRepositoryMock.Verify(x => x.GetByIdAsync(user.Id), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(email, result.Email);
            Assert.Equal(userProfileId, result.UserProfileId);
            Assert.Equal(firstName, result.FirstName);
            Assert.Equal(lastName, result.LastName);
            Assert.Equal(phoneNumber,result.PhoneNumber);
        }

        [Fact]
        public async Task GetDetailsAsyncThrowsUserNotFoundExceptionWhenUserWithIdDoesNotExist()
        {
            Setup();

            var id = Guid.NewGuid();

            await Assert.ThrowsAsync<NotFoundException>(() => _userService.GetDetailsAsync(id));
        }

        [Fact]
        public async Task UpdateProfileAsyncShouldUpdateUserProfile()
        {
            Setup();

            var userId = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var firstName = "FirstName";
            var lastName = "LastName";
            var phoneNumber = "000000000";

            var user = new User(userId, userProfileId, "test@mail.com", new byte[64], new byte[128]);
            var userProfile = new UserProfile(userProfileId, "Test", "TestName", "999999999");

            _userRepositoryMock.Setup(x => x.GetByIdAsync(user.Id)).ReturnsAsync(user);
            _userProfileRepositoryMock.Setup(x => x.GetByIdAsync(userProfile.Id)).ReturnsAsync(userProfile);


            await _userService.UpdateProfileAsync(userId, firstName, lastName, phoneNumber);


            _userRepositoryMock.Verify(x => x.GetByIdAsync(userId), Times.Once());
            _userProfileRepositoryMock.Verify(x => x.GetByIdAsync(userProfileId), Times.Once);
            Assert.Equal(firstName, userProfile.FirstName);
            Assert.Equal(lastName, userProfile.LastName);
            Assert.Equal(phoneNumber, userProfile.PhoneNumber);
        }

        [Fact]
        public async Task UpdateProfileAsyncShouldThrowUserNotFoundExceptionWhenUserWithIdDoesNotExist()
        {
            Setup();

            var id = Guid.NewGuid();
            var firstName = "John";
            var lastName = "Doe";
            var phoneNumber = "123456789";

            _userRepositoryMock.Setup(x => x.GetByIdAsync(id))!.ReturnsAsync((User)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _userService.UpdateProfileAsync(id, firstName, lastName, phoneNumber));
        }

        [Fact]
        public async Task UpdateProfileAsyncShouldThrowUserNotFoundExceptionWhenUserProfileWithIdDoesNotExist()
        {
            Setup();

            var userId = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var firstName = "John";
            var lastName = "Doe";
            var phoneNumber = "123456789";

            var user = new User(userId, userProfileId, "test@mail.com", new byte[64], new byte[128]);
            var userProfile = new UserProfile(userProfileId, firstName, lastName, phoneNumber);

            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);
            _userProfileRepositoryMock.Setup(x => x.GetByIdAsync(userProfileId))!.ReturnsAsync((UserProfile)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _userService.UpdateProfileAsync(user.Id, firstName, lastName, phoneNumber));
        }
    }
}