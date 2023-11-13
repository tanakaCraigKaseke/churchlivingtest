using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeManagementApp.Core.DataTransferObjects;
using TimeManagementApp.Core.Interfaces;
using TimeManagementApp.Core.Services;

namespace TimeManagementApp.Tests
{
    [TestClass]
    public class UserServiceTest
    {
        private IUserService userService;

        [TestInitialize]
        public void Initialize()
        {
            // Initialize the UserService instance here
            userService = new UserService();
        }

        [TestMethod]
        public void TestLoginUser_ValidCredentials()
        {
            // Arrange
            var loginDetails = new LoginDto
            {
                Username = "tanaka@test.com",
                Password = "Welcome123"
            };

            // Act
            DataResponseDto result = userService.LoginUser(loginDetails).Result;

            // Assert
            Assert.IsTrue(result.IsSuccesfull, "Login should be successful for valid credentials");
            // Add more assertions as needed
        }

        [TestMethod]
        public void TestLoginUser_InvalidCredentials()
        {
            // Arrange
            var loginDetails = new LoginDto
            {
                Username = "invalidUsername@timemanager.com",
                Password = "invalidPassword123"
            };

            // Act
            DataResponseDto result = userService.LoginUser(loginDetails).Result;

            // Assert
            Assert.IsFalse(result.IsSuccesfull, "Login should fail for invalid credentials");
            // Add more assertions as needed
        }

        [TestMethod]
        public void TestCheckIfUsernameExists_ExistingUsername()
        {
            // Arrange
            string existingUsername = "tanaka@test.com";

            // Act
            bool usernameExists = userService.CheckIfUsernameExists(existingUsername).Result;

            // Assert
            Assert.IsTrue(usernameExists, "Username should exist in the system");
        }

        [TestMethod]
        public void TestCheckIfUsernameExists_NonExistingUsername()
        {
            // Arrange
            string nonExistingUsername = "nonExistingUser@test.com";

            // Act
            bool usernameExists = userService.CheckIfUsernameExists(nonExistingUsername).Result;

            // Assert
            Assert.IsFalse(usernameExists, "Username should not exist in the system");
        }

        [TestMethod]
        public void TestGetUserById_ValidUserId()
        {
            // Arrange
            int validUserId = 1; // Provide a valid user ID that exists in your test database

            // Act
            LoggedInUserDto user = userService.GetUserById(validUserId).Result;

            // Assert
            Assert.IsNotNull(user, "User with a valid ID should be found");
            // Add more assertions as needed to validate the user properties
        }
          
        [TestMethod]
        public void TestGetUserById_InvalidUserId()
        {
            // Arrange
            int invalidUserId = -1; // Provide an invalid user ID that does not exist in your test database

            // Act
            LoggedInUserDto user = userService.GetUserById(invalidUserId).Result;

            // Assert
            Assert.IsNull(user, "User with an invalid ID should not be found");
        }
    }
}
