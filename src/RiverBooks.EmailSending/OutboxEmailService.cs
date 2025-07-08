using Ardalis.Result;

namespace RiverBooks.EmailSending;

internal class OutboxEmailService(IOutboxService outboxService, IEmailSender emailSender) : IOutboxEmailService
{
    public async Task CheckAndSendEmails(CancellationToken ct)
    {
        var result = await outboxService.GetUnprocessedEmailAsync(ct);

        if (result.Status == ResultStatus.NotFound)
            return;

        var email = result.Value;
        await emailSender.SendEmailAsync(email.To, email.From, email.Subject, email.Body, ct);

        await outboxService.MarkEmailAsProcessedAsync(email.Id, ct);
    }
}
