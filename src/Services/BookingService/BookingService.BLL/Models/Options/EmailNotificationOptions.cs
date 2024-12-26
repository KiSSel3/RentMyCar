namespace BookingService.BLL.Models.Options;

public class EmailNotificationOptions
{
    public const string SectionName = "SmtpSettings";

    public string Server { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string FromEmail { get; set; }
}