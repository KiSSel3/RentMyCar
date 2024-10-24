using AutoMapper;
using BookingService.BLL.Exceptions;
using BookingService.BLL.Infrastructure.Filters;
using BookingService.BLL.Models.DTOs.Booking;
using BookingService.BLL.Providers.Interfaces;
using BookingService.BLL.Services.Interfaces;
using BookingService.DAL.Repositories.Interfaces;
using BookingService.Domain.Entities;
using BookingService.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace BookingService.BLL.Services.Implementations;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRentOfferProvider _rentOfferProvider;
    private readonly IUserProvider _userProvider;
    private readonly ILogger<BookingService> _logger;
    private readonly IMapper _mapper;


    public BookingService(
        IBookingRepository bookingRepository,
        IRentOfferProvider rentOfferProvider,
        IUserProvider userProvider,
        ILogger<BookingService> logger,
        IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _rentOfferProvider = rentOfferProvider;
        _userProvider = userProvider;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task CreateBookingAsync(CreateBookingDTO createBookingDTO,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new booking for RentOffer {RentOfferId} and User {UserId}", 
            createBookingDTO.RentOfferId, createBookingDTO.UserId);
        
        var isUserValid = await _userProvider.IsUserValidAsync(createBookingDTO.UserId, cancellationToken);
        if (!isUserValid)
        {
            throw new EntityNotFoundException("UserEntity", createBookingDTO.UserId);
        }
        
        var rentOffer = await _rentOfferProvider.GetRentOfferById(createBookingDTO.RentOfferId, cancellationToken);
        if (rentOffer is null || !rentOffer.IsAvailable)
        {
            throw new EntityNotFoundException("RentOfferEntity", createBookingDTO.RentOfferId);
        }

        await ValidateBookingAvailabilityAsync(createBookingDTO, cancellationToken);

        var booking = _mapper.Map<BookingEntity>(createBookingDTO);

        var rentalDuration = (createBookingDTO.RentalEnd - createBookingDTO.RentalStart).TotalDays;
        booking.TotalPrice = rentOffer.PricePerDay * (decimal)rentalDuration;
        
        booking.Events = new List<EventEntity>
        {
            new EventEntity
            {
                Status = BookingStatus.Pending,
                Timestamp = DateTime.UtcNow,
                Message = createBookingDTO.Message
            }
        };
        
        await _bookingRepository.CreateAsync(booking, cancellationToken);
        
        _logger.LogInformation("Successfully created booking {BookingId} with total price {TotalPrice}", 
            booking.Id, booking.TotalPrice);
    }

    public async Task UpdateBookingAsync(Guid bookingId, UpdateBookingDTO updateBookingDTO,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating booking {BookingId} with status {Status}", 
            bookingId, updateBookingDTO.Status);
        
        var booking = await _bookingRepository.GetByIdAsync(bookingId, cancellationToken);
        if (booking is null)
        {
            throw new EntityNotFoundException(nameof(BookingEntity), bookingId);
        }
        
        booking.Events.Add(new EventEntity
        {
            Status = updateBookingDTO.Status,
            Timestamp = DateTime.UtcNow,
            Message = updateBookingDTO.Message
        });
        
        await _bookingRepository.UpdateAsync(booking, cancellationToken);
        
        _logger.LogInformation("Successfully updated booking {BookingId}", bookingId);
    }

    public async Task DeleteBookingAsync(Guid bookingId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting booking {BookingId}", bookingId);
        
        var booking = await _bookingRepository.GetByIdAsync(bookingId, cancellationToken);
        if (booking is null)
        {
            throw new EntityNotFoundException(nameof(BookingEntity), bookingId);
        }
        
        await _bookingRepository.DeleteAsync(bookingId, cancellationToken);
        
        _logger.LogInformation("Successfully deleted booking {BookingId}", bookingId);
    }

    public async Task<BookingDTO> GetBookingByIdAsync(Guid bookingId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving booking {BookingId}", bookingId);
        
        var booking = await _bookingRepository.GetByIdAsync(bookingId, cancellationToken);
        if (booking is null)
        {
            throw new EntityNotFoundException(nameof(BookingEntity), bookingId);
        }
        
        return _mapper.Map<BookingDTO>(booking);
    }

    public async Task<IEnumerable<BookingDTO>> GetBookingsAsync(BookingParametersDTO parametersDTO,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving bookings with parameters.");
        
        var filter = new BookingFilterBuilder()
            .ByUserId(parametersDTO.UserId)
            .ByRentOfferId(parametersDTO.RentOfferId)
            .ByStartDate(parametersDTO.StartDate)
            .ByEndDate(parametersDTO.EndDate)
            .Build();

        var bookings = await _bookingRepository.GetByFilterAsync(filter, cancellationToken);

        return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
    }

    public async Task<IEnumerable<DateTime>> GetAvailableDatesAsync(Guid rentOfferId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving available dates for RentOffer {RentOfferId}", rentOfferId);
        
        var rentOffer = await _rentOfferProvider.GetRentOfferById(rentOfferId, cancellationToken);
        if (rentOffer is null || !rentOffer.IsAvailable)
        {
            throw new EntityNotFoundException("RentOfferEntity", rentOfferId);
        }
        
        var bookingFilter = new BookingFilterBuilder()
            .ByRentOfferId(rentOfferId)
            .Build();

        var existingBookings = await _bookingRepository.GetByFilterAsync(bookingFilter, cancellationToken);

        var availableDates = new List<DateTime>();
        var currentDate = rentOffer.AvailableFrom.Date;
        var endDate = rentOffer.AvailableTo.Date;

        while (currentDate <= endDate)
        {
            var isDateNotAvailable = existingBookings.Any(booking =>
                currentDate >= booking.RentalStart.Date &&
                currentDate <= booking.RentalEnd.Date &&
                booking.Events.OrderByDescending(e => e.Timestamp).First().Status != BookingStatus.Canceled);

            if (!isDateNotAvailable)
            {
                availableDates.Add(currentDate);
            }

            currentDate = currentDate.AddDays(1);
        }

        _logger.LogInformation("Found {Count} available dates for RentOffer {RentOfferId}", 
            availableDates.Count, rentOfferId);
        
        return availableDates;
    }
    
    private async Task ValidateBookingAvailabilityAsync(CreateBookingDTO createBookingDTO, CancellationToken cancellationToken)
    {
        var bookingFilter = new BookingFilterBuilder()
            .ByRentOfferId(createBookingDTO.RentOfferId)
            .ByDateOverlap(createBookingDTO.RentalStart, createBookingDTO.RentalEnd)
            .Build();

        var existingBookings = await _bookingRepository.GetByFilterAsync(bookingFilter, cancellationToken);
        if (existingBookings.Any())
        {
            throw new BookingConflictException();
        }
    }
}