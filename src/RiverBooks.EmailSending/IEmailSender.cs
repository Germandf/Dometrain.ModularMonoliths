namespace RiverBooks.EmailSending;

internal interface IEmailSender
{
    Task SendEmailAsync(string to, string from, string subject, string body, CancellationToken cancellationToken);
}
