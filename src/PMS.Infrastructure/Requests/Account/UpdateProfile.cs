namespace PMS.Infrastructure.Requests.Account
{
    public class UpdateProfile
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }

        public UpdateProfile(string? firstName, string? lastName, string? phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }
    }
}