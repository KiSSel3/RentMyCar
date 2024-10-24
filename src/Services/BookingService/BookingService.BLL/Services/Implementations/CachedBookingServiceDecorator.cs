using BookingService.BLL.Models.DTOs.Booking;
using BookingService.BLL.Models.Options;
using BookingService.BLL.Providers.Interfaces;
using BookingService.BLL.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookingService.BLL.Services.Implementations;

public class CachedBookingServiceDecorator : IBookingService
{
    private readonly ICacheProvider _cacheProvider;
    private readonly ILogger<CachedBookingServiceDecorator> _logger;
    private readonly BookingService _inner;
    private readonly BookingCacheOptions _bookingCacheOptions;
    
    public CachedBookingServiceDecorator(
        ICacheProvider cacheProvider,
        ILogger<CachedBookingServiceDecorator> logger,
        BookingService inner,
        IOptions<BookingCacheOptions> cacheOptions)
    {
        _cacheProvider = cacheProvider;
        _logger = logger;
        _inner = inner;
        _bookingCacheOptions = cacheOptions.Value;
    }

    public async Task CreateBookingAsync(CreateBookingDTO createBookingDTO, CancellationToken cancellationToken = default)
    {
        var key = GetAvailableDatesKey(createBookingDTO.RentOfferId);
        
        await _inner.CreateBookingAsync(createBookingDTO, cancellationToken);
        
        _logger.LogInformation("Removing cache for available dates with key {CacheKey} after booking creation", key);
        
        await _cacheProvider.RemoveCacheDataAsync(key, cancellationToken);
    }

    public async Task UpdateBookingAsync(Guid bookingId, UpdateBookingDTO updateBookingDTO, CancellationToken cancellationToken = default)
    {
        await _inner.UpdateBookingAsync(bookingId, updateBookingDTO, cancellationToken);
        
        var booking = await _inner.GetBookingByIdAsync(bookingId, cancellationToken);
        
        var key = GetAvailableDatesKey(booking.RentOfferId);
        
        _logger.LogInformation("Removing cache for available dates with key {CacheKey} after booking creation", key);
        
        await _cacheProvider.RemoveCacheDataAsync(key, cancellationToken);
    }

    public async Task DeleteBookingAsync(Guid bookingId, CancellationToken cancellationToken = default)
    {
        await _inner.DeleteBookingAsync(bookingId, cancellationToken);
        
        var booking = await _inner.GetBookingByIdAsync(bookingId, cancellationToken);
        
        var key = GetAvailableDatesKey(booking.RentOfferId);
        
        _logger.LogInformation("Removing cache for available dates with key {CacheKey} after booking creation", key);
        
        await _cacheProvider.RemoveCacheDataAsync(key, cancellationToken);
    }

    public async Task<BookingDTO> GetBookingByIdAsync(Guid bookingId, CancellationToken cancellationToken = default)
    {
        return await _inner.GetBookingByIdAsync(bookingId, cancellationToken);
    }

    public async Task<IEnumerable<BookingDTO>> GetBookingsAsync(BookingParametersDTO parametersDTO, CancellationToken cancellationToken = default)
    {
        return await _inner.GetBookingsAsync(parametersDTO, cancellationToken);
    }

    public async Task<IEnumerable<DateTime>> GetAvailableDatesAsync(Guid rentOfferId, CancellationToken cancellationToken = default)
    {
        var key = GetAvailableDatesKey(rentOfferId);
        
        _logger.LogInformation("Attempting to get available dates from cache with key {CacheKey}", key);
        
        var cachedDates = await _cacheProvider.GetCachedDataAsync<IEnumerable<DateTime>>(key, cancellationToken);
        if (cachedDates is not null)
        {
            _logger.LogInformation("Cache hit: Retrieved available dates from cache for rent offer {RentOfferId}", rentOfferId);
            
            return cachedDates;
        }

        _logger.LogInformation("Cache miss: Available dates not found in cache for rent offer {RentOfferId}", rentOfferId);
        
        var dates = await _inner.GetAvailableDatesAsync(rentOfferId, cancellationToken);
        
        _logger.LogInformation("Setting cache for available dates with key {CacheKey}, TTL: {CacheTtl}", 
            key, _bookingCacheOptions.AvailableDatesCacheTtl);
        
        await _cacheProvider.SetCacheDataAsync(key, dates, _bookingCacheOptions.AvailableDatesCacheTtl, cancellationToken);
        
        return dates;
    }
    
    private string GetAvailableDatesKey(Guid rentOfferId)
    { 
       return string.Format(_bookingCacheOptions.AvailableDatesCacheKeyTemplate, rentOfferId);
    }
}