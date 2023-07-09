using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Dto;
using PMS.Infrastructure.Exceptions;
using PMS.Infrastructure.Extensions;
using PMS.Infrastructure.Services.Interfaces;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace PMS.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IJwtService _jwtService;



        public UserService(IUserRepository userRepository, IUserProfileRepository userProfileRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
            _jwtService = jwtService;
        }



        public async Task<UserDto> GetAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);

            return new UserDto(user.Id, user.UserProfileId, user.Role, user.Email);
        }

        public async Task<UserProfileDto> GetDetailsAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var userProfile = await _userProfileRepository.GetOrFailAsync(user.UserProfileId);

            return new UserProfileDto(user.Id, userProfile.Id, user.Role, user.Email, userProfile.FirstName, userProfile.LastName, userProfile.PhoneNumber);
        }

        public async Task RegisterAsync(string email, string password, string firstName, string lastName, string phoneNumber)
        {
            if(string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email), "Email cannot be null");

            var emailWithoutSpaces = email.Trim(' ', '\n', '\t');

            var user = await _userRepository.GetByEmailAsync(emailWithoutSpaces);

            if(user != null)
                throw new UserAlreadyExistException($"User with email '{email}' already exist");

            ValidatePassword(password);
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUserId = Guid.NewGuid();
            var newUserProfileId = Guid.NewGuid();

            user = new User(newUserId, newUserProfileId, emailWithoutSpaces, passwordHash, passwordSalt);
            var userProfile = new UserProfile(newUserProfileId,firstName, lastName, phoneNumber);

            await _userProfileRepository.CreateAsync(userProfile);
            await _userRepository.CreateAsync(user);
        }

        public async Task<JwtDto> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                throw new InvalidCredentialException("Invalid credentials");

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new InvalidCredentialException("Invalid credentials");


            var jwt = _jwtService.CreateToken(user);

            return jwt;
        }

        public async Task UpdateProfileAsync(Guid id,string firstName, string lastName, string phoneNumber)
        {
            var user = await _userRepository.GetOrFailAsync(id);
            var userProfile = await _userProfileRepository.GetOrFailAsync(user.UserProfileId);

            userProfile.UpdateFields(firstName, lastName, phoneNumber);

            await _userProfileRepository.UpdateAsync(userProfile);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), "Password cannot be empty");

            if (password.Length < 8 || password.Length > 100)
                throw new ArgumentException("The password should be between 8 and 100 characters long");

            Regex regex = new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\\s).{8,100}$");

            if (!regex.IsMatch(password))
                throw new ArgumentException("The password should contain at least 1 lowercase letter, 1 uppercase letter, 1 number, 1 special character. It should be 8-100 characters long.");
        }
    }
}