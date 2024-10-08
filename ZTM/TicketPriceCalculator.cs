public sealed class TicketPriceCalculator
{
    public static decimal Calculate(Zone zone, string userid, ICurrentUserDiscountsProvider currentUserDiscountsProvider)
    {
        var zonePrice = ZonePriceBook.GetPriceByZone(zone);
        var discount = currentUserDiscountsProvider.GetDiscount(userid);
        var discountPercentage = DiscountsPriceBook.GetDiscountPercentage(discount);
        
        zonePrice *= discountPercentage;

        return zonePrice;
    }
}