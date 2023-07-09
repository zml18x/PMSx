namespace PMS.Infrastructure.Dto
{
    public class UserProfileDto
    {
        public Guid UserId { get; set; }
        public Guid UserProfileId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }


        public UserProfileDto(Guid userId, Guid userProfileId, string role, string email, string firstName, string lastName, string phoneNumber)
        {
            UserId = userId;
            UserProfileId = userProfileId;
            Role = role;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }
    }
}