internal sealed class CurrentUserDiscountsProvider(DiscountDbContext discountDbContext) : ICurrentUserDiscountsProvider
{
    public DiscountType GetDiscount(string userId)
    {
        var discount = discountDbContext.Discounts.FirstOrDefault(d => d.UserId == userId);
        return discount?.DiscountType ?? DiscountType.None;
    }
}