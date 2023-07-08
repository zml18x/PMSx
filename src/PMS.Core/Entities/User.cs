using PMS.Core.Exceptions;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace PMS.Core.Entities
{
    public class User : Entity
    {
        public Guid UserProfileId { get; protected set; }
        public string Role { get; protected set; }
        public string Email { get; protected set; }
        public byte[] PasswordHash { get; protected set; }
        public byte[] PasswordSalt { get; protected set; }
        public bool IsActive => !DeletedAt.HasValue;



        public User(Guid userId,Guid userProfileId,string email, byte[] passwordHash, byte[] passwordSalt, string role = "User")
        {
            SetId(userId,userProfileId);
            SetEmail(email);
            SetPassword(passwordHash,passwordSalt);
            SetRole(role);
        }



        private void SetPassword(byte[] passwordHash, byte[] passwordSalt)
        {
            if (passwordHash == null || passwordSalt == null)
                throw new ArgumentNullException("passwordHash or passwordSalt", "PasswordHash and PasswordSalt cannot be null");

            if (passwordHash.Length != HMACSHA512.HashSizeInBytes || passwordSalt.Length != 128)
                throw new ArgumentException("passwordHash or passwordSalt", "Invalid length of password hash or salt");

            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        private void SetId(Guid userId, Guid userProfileId)
        {
            if (userId == Guid.Empty)
                throw new EmptyIdException("UserId cannot be empty");

            if (userProfileId == Guid.Empty)
                throw new EmptyIdException("UserProfileId cannot be empty");

            Id = userId;
            UserProfileId = userProfileId;
        }

        private void SetRole(string role)
        {
            if (string.IsNullOrEmpty(role) || string.IsNullOrWhiteSpace(role))
                throw new ArgumentNullException(nameof(role), "Role cannot be null");

            //enum ???
            var roles = new List<string>() { "Admin", "User" };

            if (!roles.Contains(role))
                throw new ArgumentException(nameof(role), "Invalid user role");

            Role = role;
        }

        private void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email), "Email cannot be null");

            if (email.Contains(' '))
                throw new ArgumentException(nameof(email), "Email cannot contains whitespace");

            Regex regex = new Regex("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$");

            if (!regex.IsMatch(email))
                throw new ArgumentException("Invalid email format", nameof(email));

            Email = email;
        }
    }
}