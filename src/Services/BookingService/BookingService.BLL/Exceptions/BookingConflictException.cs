namespace BookingService.BLL.Exceptions;

public class BookingConflictException : Exception
{
    public BookingConflictException()
        : base("Selected dates are not available") { }
    
    public BookingConflictException(string message) : base(message) { }
}