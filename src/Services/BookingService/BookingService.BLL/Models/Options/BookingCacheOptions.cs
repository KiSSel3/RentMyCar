namespace BookingService.BLL.Models.Options;

public class BookingCacheOptions
{
    public const string SectionName = "BookingCache";
    
    public TimeSpan AvailableDatesCacheTtl { get; set; }
    public string AvailableDatesCacheKeyTemplate { get; set; }
}