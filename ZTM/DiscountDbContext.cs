using Microsoft.EntityFrameworkCore;

public sealed class DiscountDbContext(DbContextOptions<DiscountDbContext> options) : DbContext(options)
{
    public DbSet<Discount> Discounts { get; set; }
}