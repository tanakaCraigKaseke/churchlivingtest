using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeManagementApp.Core.DataTransferObjects;
using TimeManagementApp.Core.Interfaces;
using TimeManagementApp.Core.Services;

namespace TimeManagementApp.Tests
{  
    [TestClass]
    public class SemesterServiceTest
    {
        private ISemesterService semesterService;
        private Mock<IUserService> userServiceMock;  

        [TestInitialize]
        public void Initialize()
        {
            // Create a mock for IUserService
            userServiceMock = new Mock<IUserService>();

            // Initialize the SemesterService instance with the mock
            semesterService = new SemesterService(userServiceMock.Object);
        }

        [TestMethod]
        public async Task TestCreateNewSemester_ValidData()
        {
            // Arrange
            var newSemester = new NewSemesterDto
            {
                UserId = 1, // Provide a valid user ID
                Name = "Spring 2023",
                Weeks = 16,
                StartDate = DateTime.Now
            };

            // Mock GetUserById to return a user
            userServiceMock.Setup(x => x.GetUserById(newSemester.UserId))
                .ReturnsAsync(new LoggedInUserDto());

            // Act
            DataResponseDto result = await semesterService.CreateNewSemester(newSemester);

            // Assert
            Assert.IsTrue(result.IsSuccesfull, "Creating a new semester with valid data should be successful");
            // Add more assertions as needed
        }

        [TestMethod]
        public async Task TestCreateNewSemester_InvalidData()
        {
            // Arrange
            var newSemester = new NewSemesterDto
            {
                UserId = -1, // Use an invalid user ID
                Name = "", // Use an empty semester name
                Weeks = -1, // Use a negative number of weeks
                StartDate = DateTime.Now // Provide a valid start date
            };

            // Mock GetUserById to return null
            userServiceMock.Setup(x => x.GetUserById(newSemester.UserId))
                .ReturnsAsync((LoggedInUserDto)null);

            // Act
            DataResponseDto result = await semesterService.CreateNewSemester(newSemester);

            // Assert
            Assert.IsFalse(result.IsSuccesfull, "Creating a new semester with invalid data should fail");
            // Add more assertions as needed
        }

        [TestMethod]
        public async Task TestGetAllSemestersForUser_ValidUserId()
        {
            // Arrange
            int validUserId = 1; // Provide a valid user ID

            // Mock GetUserById to return a user
            userServiceMock.Setup(x => x.GetUserById(validUserId))
                .ReturnsAsync(new LoggedInUserDto());

            // Mock the database query to return a list of semesters
            var semesters = new List<ExistingSemesterDto>
            {
                // Create sample semesters as needed for testing
                new ExistingSemesterDto
                {
                    SemesterId = 1,
                    Name = "Spring 2023",
                    Weeks = 16,
                    StartDate = DateTime.Now
                },
                // Add more sample semesters if needed
            };

            // Mock the database query to return the list of semesters
            // when GetUserSemesters is called with the validUserId
            userServiceMock.Setup(x => x.GetUserById(validUserId))
                .ReturnsAsync(new LoggedInUserDto());  

            // Act
            DataResponseDto result = await semesterService.GetAllSemestersForUser(validUserId);

            // Assert
            Assert.IsTrue(result.IsSuccesfull, "Getting all semesters for a valid user should be successful");
            // Add more assertions as needed to validate the semesters retrieved
        }

        [TestMethod]
        public async Task TestGetAllSemestersForUser_InvalidUserId()
        {
            // Arrange
            int invalidUserId = -1; // Provide an invalid user ID

            // Mock GetUserById to return null for an invalid user
            userServiceMock.Setup(x => x.GetUserById(invalidUserId))
                .ReturnsAsync((LoggedInUserDto)null);

            // Act
            DataResponseDto result = await semesterService.GetAllSemestersForUser(invalidUserId);

            // Assert
            Assert.IsFalse(result.IsSuccesfull, "Getting all semesters for an invalid user should fail");
        }
    }
}
  