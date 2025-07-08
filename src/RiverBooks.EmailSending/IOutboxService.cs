using Ardalis.Result;

namespace RiverBooks.EmailSending;

internal interface IOutboxService
{
    Task<Result<EmailOutbox>> GetUnprocessedEmailAsync(CancellationToken ct);
    Task MarkEmailAsProcessedAsync(Guid id, CancellationToken ct);
    Task QueueEmail(EmailOutbox email);
}
