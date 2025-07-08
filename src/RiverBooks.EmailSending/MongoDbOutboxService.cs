using Ardalis.Result;
using MongoDB.Driver;

namespace RiverBooks.EmailSending;

internal class MongoDbOutboxService(IMongoCollection<EmailOutbox> collection) : IOutboxService
{
    public async Task<Result<EmailOutbox>> GetUnprocessedEmailAsync(CancellationToken ct)
    {
        var filter = Builders<EmailOutbox>.Filter.Eq(e => e.ProcessedAt, null);
        var email = await collection.Find(filter).FirstOrDefaultAsync(ct);
        if (email is null)
            return Result.NotFound();
        return email;
    }

    public async Task MarkEmailAsProcessedAsync(Guid id, CancellationToken ct)
    {
        var filter = Builders<EmailOutbox>.Filter.Eq(e => e.Id, id);
        var update = Builders<EmailOutbox>.Update.Set(e => e.ProcessedAt, DateTime.UtcNow);
        await collection.UpdateOneAsync(filter, update, cancellationToken: ct);
    }

    public async Task QueueEmail(EmailOutbox email)
    {
        await collection.InsertOneAsync(email);
    }
}
