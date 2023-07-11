using PMS.Core.Entities;
using PMS.Core.Exceptions;

namespace PMS.Core.Tests.EntitiesTests
{
    public class UserProfileTests
    {
        [Fact]
        public void UserProfileConstructorSetsPropertiesCorrectly()
        {
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";
            var phoneNumber = "123456789";


            var userProfile = new UserProfile(id, firstName, lastName, phoneNumber);


            Assert.Equal(id, userProfile.Id);
            Assert.Equal(firstName, userProfile.FirstName);
            Assert.Equal(lastName, userProfile.LastName);
            Assert.Equal(phoneNumber, userProfile.PhoneNumber);
            Assert.True(userProfile.IsActive);
            Assert.True(userProfile.CreatedAt > DateTime.MinValue);
            Assert.True(userProfile.LastUpdateAt > DateTime.MinValue);
            Assert.True(userProfile.DeletedAt == null);
        }

        [Fact]
        public void UserProfileSetIdThrowsExceptionWhenIdIsInvalid()
        {
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";
            var phoneNumber = "123456789";


            id = Guid.Empty;
            Assert.Throws<EmptyIdException>(() => new UserProfile(id, firstName, lastName, phoneNumber));
        }

        [Fact]
        public void UserProfileSetNamesThrowsExceptionWhenFirstNameOrLastNameIsInvalid()
        {
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";
            var phoneNumber = "123456789";


            firstName = null;
            Assert.Throws<ArgumentNullException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            firstName = string.Empty;
            Assert.Throws<ArgumentNullException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            firstName = "Test";
            lastName = null;
            Assert.Throws<ArgumentNullException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            lastName = string.Empty;
            Assert.Throws<ArgumentNullException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            firstName = "T";
            lastName = "Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            firstName = "Test";
            lastName = "E";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));


            firstName = new string('a', 101);
            lastName = "Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            firstName = "Test";
            lastName = new string('a', 101);
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            firstName = " Test";
            lastName = "Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            firstName = "Test";
            lastName = " Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            firstName = "9Test";
            lastName = "Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            firstName = "Test";
            lastName = "1Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            firstName = ".Test";
            lastName = "Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            firstName = "Test";
            lastName = ",Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            firstName = "#Test";
            lastName = "Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            firstName = "Test";
            lastName = "Exa$mple";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));
        }

        [Fact]
        public void UserProfileSetPhoneNumberThrowsExceptionWhenPhoneNumberIsInvalid()
        {
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";
            var phoneNumber = "123456789";


            phoneNumber = null;
            Assert.Throws<ArgumentNullException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            phoneNumber = string.Empty;
            Assert.Throws<ArgumentNullException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            phoneNumber = new string('1', 8);
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            phoneNumber = new string('1', 10);
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            phoneNumber = "12345678a";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            phoneNumber = "P12345678";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            phoneNumber = " 12345678";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            phoneNumber = ".12345678";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            phoneNumber = "$12345678";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            phoneNumber = "123#45678";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            phoneNumber = "123?45678";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));
        }

        [Fact]
        public void UpdateUserProfileShouldChangeValuesInFields()
        {
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "TestTest";
            var phoneNumber = "123456789";

            var userProfile = new UserProfile(id,firstName, lastName, phoneNumber);

            var newFirstName = "NewTest";
            var newLastName = "NewTestTest";
            var newPhoneNumber = "987654321";


            userProfile.UpdateUserProfile(null, null, null);

            Assert.Equal(firstName, userProfile.FirstName);
            Assert.Equal(lastName, userProfile.LastName);
            Assert.Equal(phoneNumber, userProfile.PhoneNumber);


            userProfile.UpdateUserProfile(newFirstName, null, null);

            Assert.Equal(newFirstName, userProfile.FirstName);
            Assert.Equal(lastName, userProfile.LastName);
            Assert.Equal(phoneNumber, userProfile.PhoneNumber);


            userProfile.UpdateUserProfile(newFirstName, newLastName, null);

            Assert.Equal(newFirstName, userProfile.FirstName);
            Assert.Equal(newLastName, userProfile.LastName);
            Assert.Equal(phoneNumber, userProfile.PhoneNumber);


            userProfile.UpdateUserProfile(newFirstName, newLastName, newPhoneNumber);

            Assert.Equal(newFirstName, userProfile.FirstName);
            Assert.Equal(newLastName, userProfile.LastName);
            Assert.Equal(newPhoneNumber, userProfile.PhoneNumber);
        }
    }
}