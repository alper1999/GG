using System.Collections.Generic;
using Moq;
using Xunit;
using DataVisualizer.Api.Models;
using DataVisualizer.Api.Services;
using System.Linq;  // Ensure to include System.Linq for the Count() extension method

namespace DataVisualizer.Api.Tests
{
    public class CsvServiceTest
    {
        public class MockCsvService : CsvService
        {
            public List<Person> LoadCsvData()
            {
                // Provide values for all required properties
                return new List<Person>
                {
                    new Person 
                    {
                        Seq = 1, 
                        NameFirst = "John", 
                        NameLast = "Doe", 
                        Age = 30, 
                        State = "Florida", 
                        Street = "123 Main St", 
                        City = "Miami", 
                        CCNumber = "1234567890123456", 
                        Latitude = 25.7617, 
                        Longitude = -80.1918
                    },
                    new Person 
                    {
                        Seq = 2, 
                        NameFirst = "Jane", 
                        NameLast = "Smith", 
                        Age = 25, 
                        State = "California", 
                        Street = "456 Oak St", 
                        City = "Los Angeles", 
                        CCNumber = "6543210987654321", 
                        Latitude = 34.0522, 
                        Longitude = -118.2437
                    }
                };
            }
        }

        [Fact]
        public void TestMockCsvService()
        {
            // Arrange
            var mockCsvService = new Mock<CsvService>();
            mockCsvService.Setup(service => service.LoadCsvData()).Returns(new List<Person>
            {
                new Person
                {
                    Seq = 1,
                    NameFirst = "John",
                    NameLast = "Doe",
                    Age = 30,
                    State = "Florida",
                    Street = "123 Main St",
                    City = "Miami",
                    CCNumber = "1234567890123456",
                    Latitude = 25.7617,
                    Longitude = -80.1918
                }
            });

            var service = mockCsvService.Object;

            // Act
            var data = service.LoadCsvData();

            // Assert
            Assert.NotNull(data);
            Assert.True(data.Count() > 0); // Ensure there are records returned
            var firstPerson = data.FirstOrDefault();  // Safer than indexing directly
            Assert.NotNull(firstPerson);
            Assert.Equal("John", firstPerson.NameFirst); // Check the name of the first person
        }
    }
}
