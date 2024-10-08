using System.ComponentModel.DataAnnotations;

public class Discount
{
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; }
    public DiscountType DiscountType { get; set; }
}