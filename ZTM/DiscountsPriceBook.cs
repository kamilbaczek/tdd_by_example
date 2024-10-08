internal sealed class DiscountsPriceBook
{
    internal static decimal GetDiscountPercentage(DiscountType discountType)
    {
        return discountType switch
        {
            DiscountType.None => 1,
            DiscountType.Student => 0.5m,
            DiscountType.Senior => 0.5m,
            DiscountType.Child => 0.5m,
            DiscountType.Family => 0.5m,
            DiscountType.Group => 0.5m,
            DiscountType.Disabled => 0.5m,
            DiscountType.Other => 0.5m,
            _ => throw new NotImplementedException()
        };
    }
}