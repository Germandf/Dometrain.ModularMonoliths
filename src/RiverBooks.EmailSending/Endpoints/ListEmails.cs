using FastEndpoints;
using MongoDB.Driver;

namespace RiverBooks.EmailSending.Endpoints;

public record ListEmailsResponse(List<EmailOutbox> Emails);

internal class ListEmails(IMongoCollection<EmailOutbox> collection) : EndpointWithoutRequest<ListEmailsResponse>
{
    public override void Configure()
    {
        Get("/emails");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var emails = await collection.Find(_ => true).ToListAsync(ct);
        await SendOkAsync(new ListEmailsResponse(emails), cancellation: ct);
    }
}
