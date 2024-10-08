public interface ICurrentUserDiscountsProvider
{ 
    DiscountType GetDiscount(string userId);   
}