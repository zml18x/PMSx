using PMS.Core.Entities;
using PMS.Core.Exceptions;

namespace PMS.Core.Tests.EntitiesTests
{
    public class UserTests
    {
        [Fact]
        public void UserConstructorSetsPropertiesCorrectly()
        {
            var id = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var email = "test@example.com";
            var passwordHash = new byte[64];
            var passwordSalt = new byte[128];
            var role = "User";


            var user = new User(id, userProfileId, email, passwordHash, passwordSalt, role);

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
        public void UserSetPasswordThrowsExceptionWhenPasswordHashAndSaltIsInvialid()
        {
            var id = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var email = "test@example.com";
            var role = "User";


            // CASE IF passwordHash IS NULL
            byte[] passwordHash = null;
            byte[] passwordSalt = new byte[128];
            Assert.Throws<ArgumentNullException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            // CASE IF passwordSalt IS NULL
            passwordHash = new byte[64];
            passwordSalt = null;
            Assert.Throws<ArgumentNullException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            // CASE IF passwordSalt & passwordHash ARE NULL
            passwordHash = null;
            passwordSalt = null;
            Assert.Throws<ArgumentNullException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            // CASE IF passwordHash IS TOO SHORT
            passwordHash = new byte[63];
            passwordSalt = new byte[128];
            Assert.Throws<ArgumentException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            // CASE IF passwordSalt IS TOO SHORT
            passwordHash = new byte[64];
            passwordSalt = new byte[127];
            Assert.Throws<ArgumentException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            // CASE IF passwordSalt & passwordHash ARE TOO SHORT
            passwordHash = new byte[63];
            passwordSalt = new byte[127];
            Assert.Throws<ArgumentException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            // CASE IF passwordHash IS TOO LONG
            passwordHash = new byte[65];
            passwordSalt = new byte[128];
            Assert.Throws<ArgumentException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            // CASE IF passwordSalt IS TOO LONG
            passwordHash = new byte[64];
            passwordSalt = new byte[129];
            Assert.Throws<ArgumentException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            // CASE IF passwordSalt & passwordHash ARE TOO LONG
            passwordHash = new byte[65];
            passwordSalt = new byte[129];
            Assert.Throws<ArgumentException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));
        }

        [Fact]
        public void UserSetIdThrowsExceptionWhenIdAndUserProfileIdIsInvalid()
        {
            var id = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var email = "test@example.com";
            var passwordHash = new byte[64];
            var passwordSalt = new byte[128];
            var role = "User";


            // CASE IF 'Id' IS GUID EMPTY
            id = Guid.Empty;
            Assert.Throws<EmptyIdException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            // CASE IF 'UserProfileId' IS GUID EMPTY
            id = Guid.NewGuid();
            userProfileId = Guid.Empty;
            Assert.Throws<EmptyIdException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));
        }

        [Fact]
        public void UserSetRoleThrowsExceptionWhenRoleIsInvalid()
        {
            var id = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var email = "test@example.com";
            var passwordHash = new byte[64];
            var passwordSalt = new byte[128];
            var role = "User";



            // CASE IF 'Role' IS NULL
            role = null;
            Assert.Throws<ArgumentNullException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            // CASE IF 'Role' IS WHITE SPACE
            role = " ";
            Assert.Throws<ArgumentNullException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            // CASE IF 'Role' IS STRING EMPTY
            role = string.Empty;
            Assert.Throws<ArgumentNullException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            // CASE IF 'Role' IS NOT a User OR Admin
            role = "Role";
            Assert.Throws<ArgumentException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));
        }

        [Fact]
        public void UserSetEmailThrowsExceptionWhenEmailIsInvalid()
        {
            var id = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var email = "test@example.com";
            var passwordHash = new byte[64];
            var passwordSalt = new byte[128];
            var role = "User";



            // CASE IF 'Email' IS NULL
            email = null;
            Assert.Throws<ArgumentNullException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            // CASE IF 'Email' IS WHITE SPACE
            email = " ";
            Assert.Throws<ArgumentNullException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            // CASE IF 'Email' IS STRING EMPTY
            email = string.Empty;
            Assert.Throws<ArgumentNullException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));


            // CASE WHEN 'Email' IS IN THE WRONG FORMAT
            email = "test.example.com";
            Assert.Throws<ArgumentException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            email = "test@example@mail.com";
            Assert.Throws<ArgumentException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            email = "@example.com";
            Assert.Throws<ArgumentException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            email = "test@";
            Assert.Throws<ArgumentException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));

            email = "@";
            Assert.Throws<ArgumentException>(() => new User(id, userProfileId, email, passwordHash, passwordSalt, role));


            // CASE IF 'Email' CONTAINS SPACES

            var invalidMailsWithSpaces = new List<string>()
            {
                " test@example.com"," tes t@example.com"," test@exam ple.com"," test@example.co m"
            };

            foreach (var invalidMail in invalidMailsWithSpaces)
                Assert.Throws<ArgumentException>(() => new User(id, userProfileId, invalidMail, passwordHash, passwordSalt, role));


            // CASE IF 'Email' CONTAINS FORBIDDEN CHARACTERS

            var invalidMailsWithForbiddenCharacters = new List<string>()
            {
                "t#est@example.com","test@exa$ple.com","test@example.c!om","t%est@example.com",
                "t^est@example.com","t&est@example.com","t*est@example.com","t(est@example.com","t)est@example.com",
                "t+est@example.com","t=est@example.com","t~est@example.com","t,est@example.com","t?est@example.com",
                "t<est@example.com","t>est@example.com","t/est@example.com","t?est@example.com","t\\est@example.com",
                "t|est@example.com","t'est@example.com","t:est@example.com","t;est@example.com","t\"est@example.com",
                "t[est@example.com","t]est@example.com","t{est@example.com","t}est@example.com","t)est@example.com",
                "t)est@example.com","t\nest@example.com","t\test@example.com","t`est@example.com"
            };

            foreach (var invalidMail in invalidMailsWithForbiddenCharacters)
                Assert.Throws<ArgumentException>(() => new User(id, userProfileId, invalidMail, passwordHash, passwordSalt, role));
        }
    }
}