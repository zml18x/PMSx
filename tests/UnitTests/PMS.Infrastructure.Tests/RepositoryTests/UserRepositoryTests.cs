using Microsoft.EntityFrameworkCore;
using PMS.Core.Entities;
using PMS.Infrastructure.Data.Context;
using PMS.Infrastructure.Repository;

namespace PMS.Infrastructure.Tests.RepositoryTests
{
    public class UserRepositoryTests
    {
        private UserRepository _userRepository;
        private PmsDbContext _context;

        Guid id = Guid.NewGuid();
        Guid userProfileId = Guid.NewGuid();
        string email = "example@mail.com";
        byte[] passwordHash = new byte[64];
        byte[] passwordSalt = new byte[128];
        string role = "Admin";


        private void SetUp()
        {
            var options = new DbContextOptionsBuilder<PmsDbContext>()
                .UseInMemoryDatabase(databaseName: "PMS_TEST_DB")
                .Options;

            _context = new PmsDbContext(options);
            _userRepository = new UserRepository(_context);

            _context.RemoveRange(_context.Users);
            _context.SaveChanges();
        }

        [Fact]
        public async Task CreateAsyncShouldAddUser()
        {
            SetUp();

            var user = new User(id, userProfileId, email, passwordHash, passwordSalt, role);
            await _userRepository.CreateAsync(user);

            var result = await _userRepository.GetByIdAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal(user, result);

            _context.Dispose();
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateUser()
        {
            SetUp();

            var user = new User(id, userProfileId, email, passwordHash, passwordSalt, role);
            await _userRepository.CreateAsync(user);
            _context.Entry(user).State = EntityState.Detached;

            var updatedUser = new User(id, userProfileId, "updatedEmail@mail.com", passwordHash, passwordSalt, "User");
            await _userRepository.UpdateAsync(updatedUser);

            var result = await _userRepository.GetByIdAsync(user.Id);

            Assert.NotNull(result);
            Assert.NotEqual(user, result);
            Assert.Equal(user.Id, result.Id);
            Assert.NotEqual(user.LastUpdateAt, result.LastUpdateAt);
            Assert.True(user.LastUpdateAt < result.LastUpdateAt);

            _context.Dispose();
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteUserAndThrowsUserNotFoundException()
        {
            SetUp();

            var user = new User(id, userProfileId, email, passwordHash, passwordSalt, role);
            await _userRepository.CreateAsync(user);

            var usersCountBeforeDelete = _context.Users.Count();

            await _userRepository.DeleteAsync(user);

            var usersCountAfterDelete = _context.Users.Count();

            Assert.Null(await _userRepository.GetByIdAsync(user.Id));
            Assert.True(usersCountBeforeDelete > usersCountAfterDelete);

            _context.Dispose();
        }

        [Fact]
        public async Task GetByIdAsyncShouldReturnUserIfExist()
        {
            SetUp();

            var user = new User(id, userProfileId, email, passwordHash, passwordSalt, role);
            await _userRepository.CreateAsync(user);

            var result = await _userRepository.GetByIdAsync(user.Id);

            Assert.Equal(user, result);

            _context.Dispose();
        }

        [Fact]
        public async Task GetByEmailAsyncShouldReturnUserIfExist()
        {
            SetUp();

            var user = new User(id, userProfileId, email, passwordHash, passwordSalt, role);
            await _userRepository.CreateAsync(user);

            var result = await _userRepository.GetByEmailAsync(user.Email);

            Assert.Equal(user, result);

            _context.Dispose();
        }
    }
}