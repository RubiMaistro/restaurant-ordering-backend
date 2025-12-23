using Azure;
using FluentAssertions;
using Restaurant.Api.IntegrationTests.Fixtures;
using Restaurant.Api.IntergrationTests.DTOs;
using Restaurant.Api.IntergrationTests.Fixtures;
using System.Net;
using System.Net.Http.Json;

namespace Restaurant.Api.IntergrationTests.Orders
{
    public class CreateAndGetOrdersTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly HttpClient _client;

        public CreateAndGetOrdersTests(IntegrationTestFixture factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Api_should_start()
        {
            var response = await _client.GetAsync("/");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound); // API running if NotFound is returned
        }

        [Fact]
        public async Task CreateOrder_then_GetById_should_ReturnSameOrder()
        {
            // Arrange
            var createOrderRequest = new HttpRequestMessage(HttpMethod.Post, "/api/v1/Orders")
            {
                Content = JsonContent.Create(new 
                {
                    Id = Guid.NewGuid(),
                    Items = new[]
                    {
                        new { DishId = TestIds.OrderItemId1, Quantity = 2, UnitPrice = 15 },
                        new { DishId = TestIds.OrderItemId2, Quantity = 1, UnitPrice = 25 }
                    }
                })
            };
            // Act - Create Order
            var createResponse = await _client.SendAsync(createOrderRequest);
            var errorBody = await createResponse.Content.ReadAsStringAsync();
            Console.WriteLine($"Create Order Response Body: {errorBody}");

            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdOrder = await createResponse.Content.ReadFromJsonAsync<OrderResponse>();
            createdOrder.Should().NotBeNull();
            createdOrder!.Id.Should().NotBeEmpty();

            // Act - Get Order by Id
            var getRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/Orders/{createdOrder.Id}");
            var getResponse = await _client.SendAsync(getRequest);

            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var fetchedOrder = await getResponse.Content.ReadFromJsonAsync<OrderResponse>();

            fetchedOrder.Should().NotBeNull();
            fetchedOrder!.Id.Should().Be(createdOrder.Id);
            fetchedOrder.Items.Should().HaveCount(2);
        }
    }
}
