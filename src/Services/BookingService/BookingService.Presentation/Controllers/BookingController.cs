using BookingService.BLL.Models.DTOs.Booking;
using BookingService.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost("create")]
    public async Task<ActionResult> CreateBooking([FromForm] CreateBookingDTO createBookingDTO, CancellationToken cancellationToken)
    {
        await _bookingService.CreateBookingAsync(createBookingDTO, cancellationToken);
        return NoContent();
    }

    [HttpPut("update/{bookingId}")]
    public async Task<ActionResult> UpdateBooking(Guid bookingId, [FromForm] UpdateBookingDTO updateBookingDTO, CancellationToken cancellationToken)
    {
        await _bookingService.UpdateBookingAsync(bookingId, updateBookingDTO, cancellationToken);
        return NoContent();
    }

    [HttpDelete("delete/{bookingId}")]
    public async Task<ActionResult> DeleteBooking(Guid bookingId, CancellationToken cancellationToken)
    {
        await _bookingService.DeleteBookingAsync(bookingId, cancellationToken);
        return NoContent();
    }

    [HttpGet("get-by-id/{bookingId}")]
    public async Task<ActionResult> GetBookingById(Guid bookingId, CancellationToken cancellationToken)
    {
        var booking = await _bookingService.GetBookingByIdAsync(bookingId, cancellationToken);
        return Ok(booking);
    }

    [HttpPost("get-by-parameters")]
    public async Task<ActionResult> GetBookings([FromForm] BookingParametersDTO parameters, CancellationToken cancellationToken)
    {
        var bookings = await _bookingService.GetBookingsAsync(parameters, cancellationToken);
        return Ok(bookings);
    }

    [HttpGet("available-dates/{rentOfferId}")]
    public async Task<ActionResult> GetAvailableDates(Guid rentOfferId, CancellationToken cancellationToken)
    {
        var dates = await _bookingService.GetAvailableDatesAsync(rentOfferId, cancellationToken);
        return Ok(dates);
    }
}