namespace BookingService.BLL.Exceptions;

public class NotificationSendException : Exception
{
    public NotificationSendException() : base() { }
    public NotificationSendException(string message) : base(message) { }
}