using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Exceptions;
using Moq;

namespace PMS.Infrastructure.Extensions.Tests
{
    public class UserRepositoryExtensionsTests
    {
        private Mock<IUserRepository> userRepositoryMock;
        private Mock<IUserProfileRepository> userProfileRepositoryMock;


        public UserRepositoryExtensionsTests()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            userProfileRepositoryMock = new Mock<IUserProfileRepository>();
        }


        [Fact]
        public async Task GetOrFailAsync_ValidUserId_ReturnsUser()
        {
            var userId = Guid.NewGuid();
            userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
                              .ReturnsAsync(new User(userId, Guid.NewGuid(), "test@mail.com", new byte[64], new byte[128]));

            var result = await userRepositoryMock.Object.GetOrFailAsync(userId);

            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public async Task GetOrFailAsync_EmptyUserId_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => userRepositoryMock.Object.GetOrFailAsync(Guid.Empty));  
        }

        [Fact]
        public async Task GetOrFailAsync_NonExistingUserId_ThrowsUserNotFoundException()
        {
            var userId = Guid.NewGuid();


            await Assert.ThrowsAsync<UserNotFoundException>(() => userRepositoryMock.Object.GetOrFailAsync(userId));
        }

        [Fact]
        public async Task GetOrFailAsync_ValidUserProfileId_ReturnsUserProfile()
        {
            var userProfileId = Guid.NewGuid();
            userProfileRepositoryMock.Setup(repo => repo.GetByIdAsync(userProfileId))
                                    .ReturnsAsync(new UserProfile(userProfileId,"Test","TestName","123456789"));

            var result = await userProfileRepositoryMock.Object.GetOrFailAsync(userProfileId);

            Assert.NotNull(result);
            Assert.Equal(userProfileId, result.Id);
        }

        [Fact]
        public async Task GetOrFailAsync_EmptyUserProfileId_ThrowsArgumentNullException()
        {
            var userProfileRepositoryMock = new Mock<IUserProfileRepository>();


            await Assert.ThrowsAsync<ArgumentNullException>(() => userProfileRepositoryMock.Object.GetOrFailAsync(Guid.Empty));
        }

        [Fact]
        public async Task GetOrFailAsync_NonExistingUserProfileId_ThrowsUserNotFoundException()
        {
            var userProfileId = Guid.NewGuid();
            userProfileRepositoryMock.Setup(repo => repo.GetByIdAsync(userProfileId))
                                    .ReturnsAsync((UserProfile)null!);


            await Assert.ThrowsAsync<UserNotFoundException>(() => userProfileRepositoryMock.Object.GetOrFailAsync(userProfileId));
        }
    }
}
