using FluentAssertions;
using Moq;

namespace ZTM.UnitTests;

public class TicketPriceCaculatorTests
{
    [Theory]
    [InlineData(Zone.A, 2, "kamil")]
    [InlineData(Zone.Country, 1.5, "kamil")]
    public void Given_user_bought_ticket_When_choose_city_zone_AND_has_no_discount_Then_price_should_be_2(Zone zone, decimal expectedPrice, string userId)
    {
        // Arrange
        var currentUserDiscountsProviderMock = new Mock<ICurrentUserDiscountsProvider>();
        currentUserDiscountsProviderMock.Setup(x => x.GetDiscount(It.IsAny<string>())).Returns(DiscountType.None);
        
        // Act
        var price = TicketPriceCalculator.Calculate(zone, userId, currentUserDiscountsProviderMock.Object);
        
        // Assert
        price.Should().Be(expectedPrice);
    }
    
    [Fact]
    public void Given_user_bought_ticket_When_choose_city_zone_AND_has_discount_Then_price_should_be_properly_caculated()
    {
        // Arrange
        var zone = Zone.A;
        var expectedPrice = 1;
        var userId = "kamil";
        var currentUserDiscountsProviderMock = new Mock<ICurrentUserDiscountsProvider>();
        currentUserDiscountsProviderMock.Setup(x => x.GetDiscount(userId)).Returns(DiscountType.Senior);
        
        // Act
        var price = TicketPriceCalculator.Calculate(zone, userId, currentUserDiscountsProviderMock.Object);
        
        // Assert
        price.Should().Be(expectedPrice);
    }
}