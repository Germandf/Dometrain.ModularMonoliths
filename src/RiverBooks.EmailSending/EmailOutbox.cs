using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RiverBooks.EmailSending;

public class EmailOutbox
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string To { get; set; }
    public required string From { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
    public DateTime? ProcessedAt { get; set; }
}
