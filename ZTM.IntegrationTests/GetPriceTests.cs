using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZTM.IntegrationTests.Common;

namespace ZTM.IntegrationTests;

public sealed class GetPriceTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly DatabaseContainer _databaseContainer;
    private readonly HttpClient _httpClient;

    public GetPriceTests(WebApplicationFactory<Program> factory, DatabaseContainer databaseContainer)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                var inMemorySettings = new Dictionary<string, string>
                {
                    { "ConnectionStrings:PostgresConnection", _databaseContainer.ConnectionString }
                };

                config.AddInMemoryCollection(inMemorySettings);
            });
        });
        _databaseContainer = databaseContainer;
        _httpClient = _factory.CreateClient();
    }

    [Fact]
    public async Task Given_get_ticket_price_for_passanger_based_on_zone_Then_price_should_be_caculated_and_returned()
    {
        // Arrange
        var userid = "kamil";
        var zone = "A";
        using var scope = _factory.Services.CreateScope();
        scope.ServiceProvider.GetRequiredService<DiscountDbContext>().Discounts.Add(new Discount { UserId = userid, DiscountType = DiscountType.None });
        
        // Act
        var httpResponseMessage = await _httpClient.GetAsync($"tickets/{userid}/{zone}");
        
        // Assert
        var ticketPriceResponse = await httpResponseMessage.Content.ReadFromJsonAsync<TicketPriceResponse>();
        ticketPriceResponse.Price.Should().Be(2);
    }
}