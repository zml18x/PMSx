using PMS.Core.Entities;

namespace PMS.Core.Tests.EntitiesTests
{
    public class UserTests
    {
        [Fact]
        public void UserConstructorSetsPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var email = "test@mail.com";
            var passwordHash = new byte[64];
            var passwordSalt = new byte[128];
            var role = "User";

            // Act
            var user = new User(id, userProfileId, email, passwordHash, passwordSalt, role);

            // Assert
            Assert.Equal(id, user.Id);
            Assert.Equal(userProfileId, user.UserProfileId);
            Assert.Equal(email, user.Email);
            Assert.Equal(passwordHash, user.PasswordHash);
            Assert.Equal(passwordSalt, user.PasswordSalt);
            Assert.Equal(role, user.Role);
            Assert.True(user.IsActive);
            Assert.True(user.CreatedAt > DateTime.MinValue);
            Assert.True(user.LastUpdateAt > DateTime.MinValue);
            Assert.True(user.DeletedAt == null);
        }

        [Fact]
        public void UserSetPasswordThrowsExceptionWhenPasswordHashOrSaltAreInvialid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var email = "test@mail.com";
            var role = "User";

            // Act
            var validPasswordHash = new byte[64];
            var validPasswordSalt = new byte[128];
            var invalidPasswordHash = new byte[63];
            var invalidPasswordSalt = new byte[127];
            byte[]? nullPasswordHash = null;
            byte[]? nullPasswordSalt = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => new User(id, userProfileId, email, nullPasswordHash!, validPasswordSalt, role));
            Assert.Throws<ArgumentNullException>(() => new User(id, userProfileId, email, validPasswordHash, nullPasswordSalt!, role));
            Assert.Throws<ArgumentNullException>(() => new User(id, userProfileId, email, nullPasswordHash!, nullPasswordSalt!, role));
            Assert.Throws<ArgumentException>(() => new User(id, userProfileId, email, invalidPasswordHash, validPasswordSalt, role));
            Assert.Throws<ArgumentException>(() => new User(id, userProfileId, email, validPasswordHash, invalidPasswordSalt, role));
            Assert.Throws<ArgumentException>(() => new User(id, userProfileId, email, invalidPasswordHash, invalidPasswordSalt, role));
        }

        [Fact]
        public void UserSetIdThrowsArgumentExceptionWhenUserIdIsInvalid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var email = "test@example.com";
            var passwordHash = new byte[64];
            var passwordSalt = new byte[128];
            var role = "User";

            // Act
            var invalidUserId = Guid.Empty;

            // Assert
            Assert.Throws<ArgumentException>(() => new User(invalidUserId, userProfileId, email, passwordHash, passwordSalt, role));
        }

        [Fact]
        public void UserSetIdThrowsArgumentExceptionWhenUserProfileIdIsInvalid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var email = "test@example.com";
            var passwordHash = new byte[64];
            var passwordSalt = new byte[128];
            var role = "User";

            // Act
            var invalidUserProfileId = Guid.Empty;

            // Assert
            Assert.Throws<ArgumentException>(() => new User(id, invalidUserProfileId, email, passwordHash, passwordSalt, role));
        }

        [Fact]
        public void UserSetRoleThrowsExceptionWhenRoleIsInvalid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var email = "test@example.com";
            var passwordHash = new byte[64];
            var passwordSalt = new byte[128];

            // Act
            var invalidRoles = new string[] { null!, " ", string.Empty };

            // Assert
            foreach (var invalidRole in invalidRoles)
                Assert.Throws<ArgumentNullException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, invalidRole));

            Assert.Throws<ArgumentException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, "Role"));
        }

        [Fact]
        public void UserSetEmailThrowsExceptionWhenEmailIsInvalid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var passwordHash = new byte[64];
            var passwordSalt = new byte[128];
            var role = "User";

            // Act
            var invalidEmailsWithSpacesOrNullOrStringEmpty = new string[] { null!, " ", string.Empty };
            var invalidEmailsInWrongFormat = new string[] { "testexample.com", "test@example@mail.com", "@example.com", "test@", "@" };
            var invalidEmailsWithSpaces = new string[] { " test@example.com", " tes t@example.com", " test@exam ple.com", " test@example.co m" };
            var invalidEmailsWithForbiddenCharacters = new string[]
            {
                "t#est@example.com","test@exa$ple.com","test@example.c!om","t%est@example.com",
                "t^est@example.com","t&est@example.com","t*est@example.com","t(est@example.com","t)est@example.com",
                "t+est@example.com","t=est@example.com","t~est@example.com","t,est@example.com","t?est@example.com",
                "t<est@example.com","t>est@example.com","t/est@example.com","t?est@example.com","t\\est@example.com",
                "t|est@example.com","t'est@example.com","t:est@example.com","t;est@example.com","t\"est@example.com",
                "t[est@example.com","t]est@example.com","t{est@example.com","t}est@example.com","t)est@example.com",
                "t)est@example.com","t\nest@example.com","t\test@example.com","t`est@example.com"
            };

            // Assert
            foreach (var invalidEmail in invalidEmailsWithSpacesOrNullOrStringEmpty)
                Assert.Throws<ArgumentNullException>(() => new User(id, userProfileId, invalidEmail, passwordHash, passwordSalt, role));

            foreach (var invalidEmail in invalidEmailsInWrongFormat)
                Assert.Throws<ArgumentException>(() => new User(id, userProfileId, invalidEmail, passwordHash, passwordSalt, role));

            foreach (var invalidMail in invalidEmailsWithSpaces)
                Assert.Throws<ArgumentException>(() => new User(id, userProfileId, invalidMail, passwordHash, passwordSalt, role));

            foreach (var invalidMail in invalidEmailsWithForbiddenCharacters)
                Assert.Throws<ArgumentException>(() => new User(id, userProfileId, invalidMail, passwordHash, passwordSalt, role));
        }
    }
}