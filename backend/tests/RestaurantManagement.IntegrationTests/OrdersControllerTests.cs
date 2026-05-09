using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace RestaurantManagement.IntegrationTests
{
    public class OrdersControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public OrdersControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateOrder_ReturnsSuccess()
        {
            // Arrange
            var command = new
            {
                TableId = (Guid?)null,
                CustomerId = (Guid?)null,
                EmployeeId = Guid.NewGuid(),
                OrderItems = new[]
                {
                    new
                    {
                        MenuItemId = Guid.NewGuid(),
                        Quantity = 2,
                        UnitPrice = 10.0m
                    }
                }
            };
            // Act
            var response = await _client.PostAsJsonAsync("/api/v1/orders", command);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
