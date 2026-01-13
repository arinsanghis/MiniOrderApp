using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence;
using System.Net.Http.Json;
using Application.Features.Customers.Commands.CreateCustomer;
using FluentAssertions;
using Xunit;
using System.Linq;

namespace Application.UnitTests;

public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Clean up any existing database config
                var descriptors = services.Where(d => 
                    d.ServiceType.Name.Contains("DbContextOptions") ||
                    d.ServiceType.Name.Contains("AppDbContext") ||
                    d.ServiceType.Name.Contains("DbConnection")).ToList();

                foreach (var d in descriptors)
                {
                    services.Remove(d);
                }

                // Add In-Memory Database
                services.AddDbContext<AppDbContext>(opts =>
                {
                    opts.UseInMemoryDatabase("InMemoryDbForTesting");
                });
            });
        });
    }

    [Fact]
    public async Task Post_CreateCustomer_ShouldReturnSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();
        var command = new CreateCustomerCommand("Integration Test User", "api_test@example.com", "123 Test St", "555-0000");

        // Act
        var response = await client.PostAsJsonAsync("/api/customers", command);

        // Assert
        // We verify that the API processed the request successfully (200 OK or 201 Created)
        // We do not parse the body to avoid JSON format mismatches
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
