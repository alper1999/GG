using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Text.Json;
using System.Collections.Generic;
using DataVisualizer.Api.Models;
namespace DataVisualizer.Api.Tests

{
    public class DataControllerTests : IClassFixture<WebApplicationFactory<Program>>  
    {
        private readonly HttpClient _client;

        public DataControllerTests(WebApplicationFactory<Program> factory) 
        {
            _client = factory.CreateClient(); 
        }

        [Fact]
        public async Task GetAllData_ReturnsOkResponse()
        {
            // Act
            var response = await _client.GetAsync("/api/data");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);

            var content = await response.Content.ReadAsStringAsync();
            // Optionally, deserialize and verify the response
            var data = JsonSerializer.Deserialize<List<Person>>(content);
            Assert.NotNull(data);
            Assert.True(data.Count > 0);  // Ensure there are records returned
        }

        [Fact]
        public async Task SearchByName_ReturnsFilteredResults()
        {
            // Act
            var response = await _client.GetAsync("/api/data/search?firstName=John");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("John", content); // Ensure the first name appears in the response
        }

        [Fact]
        public async Task FilterByState_ReturnsFilteredResults()
        {
            // Act
            var response = await _client.GetAsync("/api/data/filter?state=Florida");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Florida", content); // Ensure the state appears in the response
        }

        [Fact]
        public async Task GetStatistics_ReturnsStateStatistics()
        {
            // Act
            var response = await _client.GetAsync("/api/data/statistics");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("State", content); // Ensure the statistics contain "State"
            Assert.Contains("AverageAge", content); // Ensure average age is calculated
        }
    }
}
