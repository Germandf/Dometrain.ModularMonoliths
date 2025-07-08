namespace RiverBooks.EmailSending;

internal interface IOutboxEmailService
{
    Task CheckAndSendEmails(CancellationToken ct);
}
