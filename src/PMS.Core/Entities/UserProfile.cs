using PMS.Core.Exceptions;
using System.Text.RegularExpressions;

namespace PMS.Core.Entities
{
    public class UserProfile : Entity
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string PhoneNumber { get; protected set; }
        public bool IsActive => !DeletedAt.HasValue;



        public UserProfile(Guid id,string firstName,string lastName,string phoneNumber)
        {
            SetId(id);
            SetNames(firstName, lastName);
            SetPhoneNumber(phoneNumber);
        }


        private void SetId(Guid id)
        {
            if (id == Guid.Empty)
                throw new EmptyIdException("Id cannot be empty");

            Id = id;
        }



        private void SetNames(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || firstName == string.Empty || lastName == string.Empty)
                throw new ArgumentNullException("The names fields cannot be empty");

            if (firstName.Length < 2 || lastName.Length < 2)
                throw new ArgumentException("First name & last name must be at least 2 characters long");

            if (firstName.Length > 100 || lastName.Length > 100)
                throw new ArgumentException("First name & last name can be up to 100 characters long.");

            Regex regex = new Regex("^[a-zA-Z]+$");

            if (!regex.IsMatch(firstName) || !regex.IsMatch(lastName))
                throw new ArgumentException("First name or last name contains prohibited characters");

            FirstName = firstName;
            LastName = lastName;
        }

        private void SetPhoneNumber(string phoneNumber)
        {
            // Validation for the Polish number standard

            if (string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber), "Number phone cannot be null or white space");

            if (phoneNumber.Length != 9)
                throw new ArgumentException("The number should consist of 9 digits", nameof(phoneNumber));

            Regex regex = new Regex("^[0-9]+$");

            if (!regex.IsMatch(phoneNumber))
                throw new ArgumentException("The number can only consist of digits", nameof(phoneNumber));

            PhoneNumber = phoneNumber;
        }

        public void UpdateFields(string? firstName, string? lastName, string? phoneNumber)
        {
            var newFirstName = firstName != null ? firstName : FirstName;
            var newLastName = lastName != null ? lastName : LastName;
            var newPhoneNumber = phoneNumber != null ? phoneNumber : PhoneNumber;

            SetNames(newFirstName, newLastName);
            SetPhoneNumber(newPhoneNumber);

            LastUpdateAt = DateTime.UtcNow;
        }
    }
}