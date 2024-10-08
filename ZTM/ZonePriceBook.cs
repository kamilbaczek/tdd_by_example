internal sealed class ZonePriceBook
{
    internal static decimal GetPriceByZone(Zone zone)
    {
        return zone switch
        {
            Zone.A => 2,
            Zone.Country => 1.5m,
            _ => throw new NotImplementedException()
        };
    }
}