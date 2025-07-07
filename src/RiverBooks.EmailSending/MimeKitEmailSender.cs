using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace RiverBooks.EmailSending;

public class MimeKitEmailSender(
    ILogger<MimeKitEmailSender> logger)
    : IEmailSender
{
    public async Task SendEmailAsync(string to, string from, string subject, string body, CancellationToken cancellationToken)
    {
        logger.LogInformation("Sending email from {From} to {To} with subject {Subject}", from, to, subject);
        using var client = new SmtpClient();
        await client.ConnectAsync("localhost", 25, false, cancellationToken);
        var message = new MimeMessage
        {
            From = { MailboxAddress.Parse(from) },
            To = { MailboxAddress.Parse(to) },
            Subject = subject,
            Body = new TextPart("plain") { Text = body }
        };
        await client.SendAsync(message, cancellationToken);
        await client.DisconnectAsync(true, cancellationToken);
        logger.LogInformation("Email sent successfully");
    }
}
