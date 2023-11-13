using PMS.Core.Entities;
using Xunit.Sdk;

namespace PMS.Core.Tests.EntitiesTests
{
    public class UserProfileTests
    {
        [Fact]
        public void UserProfile_ConstructorSetsPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "TestName";
            var lastName = "TestLastName";
            var phoneNumber = "123456789";

            // Act
            var userProfile = new UserProfile(id, firstName, lastName, phoneNumber);

            // Assert
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
        public void UserProfile_SetId_ThrowsArgumentExceptionWhenUserIdIsInvalid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";
            var phoneNumber = "123456789";

            // Act
            var invalidUserProfileId = Guid.Empty;

            // Assert
            Assert.Throws<ArgumentException>(() => new UserProfile(invalidUserProfileId, firstName, lastName, phoneNumber));
        }

        [Fact]
        public void UserProfile_SetNames_ThrowsExceptionWhenFirstNameIsInvalid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var lastName = "Example";
            var phoneNumber = "123456789";

            // Act
            var invalidToLongFirstName = new string('a', 101);
            var invalidFirstNamesNullOrEmptyOrWhiteSpace = new string[] { null!, string.Empty, " " };
            var invalidFirstNames = new string[] { "T", " TestName", "9TestName", ".TestName", ",TestName", "#TestName", "Te$stName", "Test*Name", "Test(Name)", "T!ESTName", "T>ESTName", "Test:Name" };

            // Assert
            Assert.Throws<ArgumentException>(() => new UserProfile(id, invalidToLongFirstName, lastName, phoneNumber));

            foreach (var invalidFirstName in invalidFirstNamesNullOrEmptyOrWhiteSpace)
                Assert.Throws<ArgumentNullException>(() => new UserProfile(id, invalidFirstName, lastName, phoneNumber));

            foreach (var invalidFirstName in invalidFirstNames)
                Assert.Throws<ArgumentException>(() => new UserProfile(id, invalidFirstName, lastName, phoneNumber));
        }

        [Fact]
        public void UserProfile_SetNames_ThrowsExceptionWhenLastNameIsInvalid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "Test";
            var phoneNumber = "123456789";

            // Act
            var invalidToLongLasttName = new string('a', 101);
            var invalidLastNamesNullOrEmptyOrWhiteSpace = new string[] { null!, string.Empty, " " };
            var invalidLastNames = new string[] { "T", " TestName", "9TestName", ".TestName", ",TestName", "#TestName", "Te$stName", "Test*Name", "Test(Name)", "T!ESTName", "T>ESTName", "Test:Name" };

            // Assert
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, invalidToLongLasttName, phoneNumber));

            foreach (var invalidLastName in invalidLastNamesNullOrEmptyOrWhiteSpace)
                Assert.Throws<ArgumentNullException>(() => new UserProfile(id, firstName, invalidLastName, phoneNumber));

            foreach (var invalidLastName in invalidLastNames)
                Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, invalidLastName, phoneNumber));
        }

        [Fact]
        public void UserProfile_SetPhoneNumber_ThrowsExceptionWhenPhoneNumberIsInvalid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";


            // Act
            var invalidPhoneNumbersNullOrEmptyOrWhiteSpace = new string[] { null!, string.Empty, " " };
            var invalidPhoneNumbersWrongLength = new string[] { new string('1', 8), new string('1', 10) };
            var invalidPhoneNumbers = new string[] { "12345678a", "P12345678", "1234x1234", " 12345678", ".12345678", "$12345678", "123#45678", "123?45678", "123(45678", "1234:1234", ">12345678" };


            // Assert
            foreach (var invalidPhoneNumber in invalidPhoneNumbersNullOrEmptyOrWhiteSpace)
                Assert.Throws<ArgumentNullException>(() => new UserProfile(id, firstName, lastName, invalidPhoneNumber));

            foreach (var invalidPhoneNumber in invalidPhoneNumbersWrongLength)
                Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, invalidPhoneNumber));

            foreach (var invalidPhoneNumber in invalidPhoneNumbers)
                Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, invalidPhoneNumber));
        }

        [Fact]
        public void UpdateUserProfile_DoesNotUpdateWhenAllFieldsAreNulll()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";
            var phoneNumber = "000000000";

            var userProfile = new UserProfile(id, firstName, lastName, phoneNumber);


            // Act
            userProfile.UpdateUserProfile(null, null, null);


            // Assert
            Assert.Equal(phoneNumber, userProfile.PhoneNumber);
            Assert.Equal(firstName, userProfile.FirstName);
            Assert.Equal(lastName, userProfile.LastName);
        }

        [Fact]
        public void UpdateUserProfile_ShouldUpdateAllFieldsWhenAllFieldsAreNotNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";
            var phoneNumber = "000000000";

            var userProfile = new UserProfile(id, firstName, lastName, phoneNumber);

            var newFirstName = "NewName";
            var newLastName = "NewLastName";
            var newPhoneNumber = "123456789";


            // Act
            userProfile.UpdateUserProfile(newFirstName, newLastName, newPhoneNumber);


            // Assert
            Assert.Equal(newPhoneNumber, userProfile.PhoneNumber);
            Assert.Equal(newFirstName, userProfile.FirstName);
            Assert.Equal(newLastName, userProfile.LastName);
        }

        [Fact]
        public void UpdateUserProfile_UpdatesPhoneNumberWhenPhoneNumberIsNotNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";
            var phoneNumber = "000000000";
            var newPhoneNumber = "123456789";

            var userProfile = new UserProfile(id, firstName, lastName, phoneNumber);


            // Act
            userProfile.UpdateUserProfile(null, null, newPhoneNumber);


            // Assert
            Assert.Equal(newPhoneNumber, userProfile.PhoneNumber);
        }



        [Fact]
        public void UpdateUserProfile_DoesNotUpdatePhoneNumberWhenPhoneNumberIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";
            var phoneNumber = "000000000";

            var userProfile = new UserProfile(id, firstName, lastName, phoneNumber);


            // Act
            userProfile.UpdateUserProfile("NewName", "NewLastName", null);


            // Assert
            Assert.Equal(phoneNumber, userProfile.PhoneNumber);
        }

        [Fact]
        public void UpdateUserProfile_UpdatesFirstNamerWhenFirstNameIsNotNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";
            var phoneNumber = "000000000";
            var newFirstName = "NewName";

            var userProfile = new UserProfile(id, firstName, lastName, phoneNumber);


            // Act
            userProfile.UpdateUserProfile(newFirstName, null, null);


            // Assert
            Assert.Equal(newFirstName, userProfile.FirstName);
        }

        [Fact]
        public void UpdateUserProfile_DoesNotUpdateFirstNameWhenFirstNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";
            var phoneNumber = "000000000";

            var userProfile = new UserProfile(id, firstName, lastName, phoneNumber);


            // Act
            userProfile.UpdateUserProfile(null, "NewLastName", "123456789");


            // Assert
            Assert.Equal(firstName, userProfile.FirstName);
        }

        [Fact]
        public void UpdateUserProfile_UpdatesLastNamerWhenLastNameIsNotNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";
            var phoneNumber = "000000000";
            var newLastName = "NewName";

            var userProfile = new UserProfile(id, firstName, lastName, phoneNumber);


            // Act
            userProfile.UpdateUserProfile(null, newLastName, null);


            // Assert
            Assert.Equal(newLastName, userProfile.LastName);
        }

        [Fact]
        public void UpdateUserProfile_DoesNotUpdateLastNameWhenLastNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";
            var phoneNumber = "000000000";

            var userProfile = new UserProfile(id, firstName, lastName, phoneNumber);


            // Act
            userProfile.UpdateUserProfile("NewName", null, "123456789");


            // Assert
            Assert.Equal(lastName, userProfile.LastName);
        }
    }
}